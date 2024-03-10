using HRMS.Domain.Constants;
using HRMS.Domain.Entities;
using HRMS.Application.DTOs.Employees;
using HRMS.Application.Utility.Handler;
using Grade = HRMS.Domain.Entities.Grade;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Services;

public class EmployeeService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeJobRepository _employeeJobRepository;
    private readonly IEmployeeProjectRepository _employeeProjectRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IRoleRepository _roleRepository;

    public EmployeeService(IAccountRepository accountRepository, 
        IAccountRoleRepository accountRoleRepository,
        IEmployeeRepository employeeRepository, 
        IGradeRepository gradeRepository, 
        IJobRepository jobRepository,
        IEmployeeJobRepository employeeJobRepository,
        IProfileRepository profileRepository, 
        IRoleRepository roleRepository, 
        IProjectRepository projectRepository,
        IEmployeeProjectRepository employeeProjectRepository)
    {
        _accountRepository = accountRepository;
        _accountRoleRepository = accountRoleRepository;
        _employeeRepository = employeeRepository;
        _employeeJobRepository = employeeJobRepository;
        _gradeRepository = gradeRepository;
        _jobRepository = jobRepository;
        _profileRepository = profileRepository;
        _roleRepository = roleRepository;
        _projectRepository = projectRepository;
        _employeeProjectRepository = employeeProjectRepository;
    }

    public IEnumerable<EmployeeDtoGet> Get()
    {
        var employees = _employeeRepository.GetAll().ToList();
        
        return !employees.Any() ? Enumerable.Empty<EmployeeDtoGet>() : employees.Cast<EmployeeDtoGet>().ToList();
    }

    public EmployeeDtoGet? Get(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        
        if (employee is null) return null!;

        return (EmployeeDtoGet)employee;
    }

    public EmployeeDtoCreate? Create(EmployeeDtoCreate employeeDtoCreate)
    {
        Employee employee = employeeDtoCreate;
        
        employee.Code = GenerateHandler.GenerateNik(_employeeRepository.GetLastEmployeeCode());

        var grade = new Grade
        {
            Id = Guid.NewGuid(),
            GradeLevel = HRMS.Domain.Constants.Grade.B,
            FirstScoreSegment = 0,
            SecondScoreSegment = 0,
            ThirdScoreSegment = 0,
            FourthScoreSegment = 0,
            TotalScore = 0,
            CreatedAt = DateTime.Now
        };

        var gradeCreated = _gradeRepository.Create(grade);
        
        employee.GradeId = gradeCreated.Id;

        var profile = new Profile
        {
            Id = Guid.NewGuid(),
            ProfileImage = "",
            Skills = "",
            LinkedIn = "",
            ResumeURL = "",
            CreatedAt = DateTime.Now
        };

        var profileCreated = _profileRepository.Create(profile);
        
        employee.ProfileId = profileCreated.Id;

        var employeeCreated = _employeeRepository.Create(employee);

        var randomPassword = GenerateHandler.GenerateRandomString(10);
        
        var account = new Account
        {
            Id = employee.Id,
            Password = HashingHandler.HashPassword(randomPassword),
            IsDeleted = false,
            IsUsed = false,
            OTP = 0,
            CreatedAt = DateTime.Now,
            ExpiredTime = DateTime.Now.AddMinutes(10)
        };
        
        var accountCreated = _accountRepository.Create(account);

        var roleEmployee = _roleRepository.GetByName("Employee");
        
        var accountRoleCreated = _accountRoleRepository.Create(new AccountRole
        {
            AccountId = account.Id,
            RoleId = roleEmployee.Id
        });

        if (employeeCreated is not null) return (EmployeeDtoCreate)employeeCreated;
        
        _gradeRepository.Delete(gradeCreated);
            
        _profileRepository.Delete(profileCreated);
            
        _accountRepository.Delete(accountCreated);
            
        _accountRoleRepository.Delete(accountRoleCreated);
            
        return null!;
    }

    public int Update(EmployeeDtoUpdate employeeDtoUpdate)
    {
        var employee = _employeeRepository.GetByGuid(employeeDtoUpdate.Id);
        
        if (employee is null) return -1;

        var employeeUpdated = _employeeRepository.Update(employeeDtoUpdate);
        
        return !employeeUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        
        if (employee is null) return -1;

        var gradeGuid = employee.GradeId;
        
        var profileGuid = employee.ProfileId;

        var employeeDeleted = _employeeRepository.Delete(employee);

        if (employeeDeleted)
        {
            var grade = _gradeRepository.GetByGuid((Guid)gradeGuid);
            
            if (grade != null)
            {
                _gradeRepository.Delete(grade);
            }

            var profile = _profileRepository.GetByGuid((Guid)profileGuid);
            
            if (profile != null)
            {
                _profileRepository.Delete(profile);
            }

            return 1;
        }
        return 0;
    }

    public IEnumerable<EmployeeDtoGet> GetByRole(Guid roleGuid)
    {
        var employeesByRole = (from employee in _employeeRepository.GetAll()
                               join account in _accountRepository.GetAll() 
                                   on employee.Id equals account.Id
                               join accountRole in _accountRoleRepository.GetAll() 
                                   on account.Id equals accountRole.AccountId
                               join roleRepository in _roleRepository.GetAll() 
                                   on accountRole.RoleId equals roleRepository.Id
                               where accountRole.RoleId == roleGuid
                               select new EmployeeDtoGet()
                               {
                                   Id = employee.Id,
                                   Code = employee.Code,
                                   FirstName = employee.FirstName,
                                   LastName = employee.LastName,
                                   BirthDate = employee.DateOfBirth,
                                   Gender = employee.Gender,
                                   HiringDate = employee.HiredDate,
                                   Email = employee.Email,
                                   PhoneNumber = employee.PhoneNumber,
                                   Status = employee.Status,
                                   GradeId = employee.GradeId,
                                   ProfileId = employee.ProfileId,
                               }).ToList();

        return employeesByRole;
    }

    public IEnumerable<EmployeeDtoGet> GetExcludeRole(Guid roleGuid)
    {
        var employeesByRole = GetByRole(roleGuid);
        
        var employees = _employeeRepository.GetAll();

        var employeesByRoleGuid = employeesByRole.Select(employee => employee.Id).ToList();
        
        var employeesExcludeRole = employees.Where(employee => !employeesByRoleGuid.Contains(employee.Id)).ToList();

        return !employeesExcludeRole.Any() ? Enumerable.Empty<EmployeeDtoGet>() : employeesExcludeRole.Cast<EmployeeDtoGet>().ToList();
    }

    public EmployeeDtoGet? GetByEmail(string email)
    {
        var employee = _employeeRepository.GetByEmail(email);
        
        if (employee is null) return null!;

        return (EmployeeDtoGet)employee;
    }

    public IEnumerable<EmployeeDtoGet> GetEmployeeByProject(Guid projectGuid)
    {
        var employeesByProject = (from employee in _employeeRepository.GetAll()
                                  join employeeProject in _employeeProjectRepository.GetAll()
                                      on employee.Id equals employeeProject.EmployeeId
                                  join project in _projectRepository.GetAll()
                                      on employeeProject.ProjectId equals project.Id
                                  where employeeProject.ProjectId == projectGuid
                                  select new EmployeeDtoGet()
                                  {
                                      Id = employee.Id,
                                      Code = employee.Code,
                                      FirstName = employee.FirstName,
                                      LastName = employee.LastName,
                                      BirthDate = employee.DateOfBirth,
                                      Gender = employee.Gender,
                                      HiringDate = employee.HiredDate,
                                      Email = employee.Email,
                                      PhoneNumber = employee.PhoneNumber,
                                      Status = employee.Status,
                                      GradeId = employee.GradeId,
                                      ProfileId = employee.ProfileId,
                                  }).ToList();
        return employeesByProject;
    }

    public IEnumerable<EmployeeDtoGet> GetExcludeProject(Guid projectGuid)
    {
        var employeesByProject = GetEmployeeByProject(projectGuid);
        
        var employees = _employeeRepository.GetAll();

        var employeesByProjectGuid = employeesByProject.Select(employee => employee.Id).ToList();
        
        var employeesExcludeProject = employees.Where(employee => !employeesByProjectGuid.Contains(employee.Id)).ToList();

        if (!employeesExcludeProject.Any()) return Enumerable.Empty<EmployeeDtoGet>();

        return employeesExcludeProject.Where(employee => employee.Status == ActionStatus.Idle).Cast<EmployeeDtoGet>().ToList();
    }

    public IEnumerable<EmployeeDtoGet> GetEmployeeByJob(Guid jobGuid)
    {
        var employeesByProject = (from employee in _employeeRepository.GetAll()
                                  join employeeJob in _employeeJobRepository.GetAll()
                                      on employee.Id equals employeeJob.EmployeeId
                                  join job in _jobRepository.GetAll()
                                      on employeeJob.JobId equals job.Id
                                  where employeeJob.JobId == jobGuid
                                  select new EmployeeDtoGet()
                                  {
                                      Id = employee.Id,
                                      Code = employee.Code,
                                      FirstName = employee.FirstName,
                                      LastName = employee.LastName,
                                      BirthDate = employee.DateOfBirth,
                                      Gender = employee.Gender,
                                      HiringDate = employee.HiredDate,
                                      Email = employee.Email,
                                      PhoneNumber = employee.PhoneNumber,
                                      Status = employee.Status,
                                      GradeId = employee.GradeId,
                                      ProfileId = employee.ProfileId,
                                  }).ToList();
        return employeesByProject;
    }

    public IEnumerable<EmployeeDtoGet> GetExcludeJob(Guid jobGuid)
    {
        var employeesByJob = GetEmployeeByJob(jobGuid);
        
        var employees = _employeeRepository.GetAll();

        var employeesByJobGuid = employeesByJob.Select(employee => employee.Id).ToList();
        
        var employeesExcludeJob = employees.Where(employee => !employeesByJobGuid.Contains(employee.Id)).ToList();

        if (!employeesExcludeJob.Any()) return Enumerable.Empty<EmployeeDtoGet>();

        return employeesExcludeJob.Where(employee => employee.Status == ActionStatus.Idle).Cast<EmployeeDtoGet>().ToList();
    }
}
