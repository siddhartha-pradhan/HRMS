using System.Security.Claims;
using HRMS.Application.DTOs.Accounts;
using HRMS.Application.Interfaces.Contract;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Utility.Handler;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using Grade = HRMS.Domain.Entities.Grade;

namespace HRMS.Infrastructure.Implementation.Services;

public class AccountService
{
    private readonly IEmailHandler _emailHandler;
    private readonly ApplicationDbContext _context;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenHandler _tokenHandler;

    public AccountService(IAccountRepository accountRepository, 
        IEmailHandler emailHandler,
        IAccountRoleRepository accountRoleRepository,
        IEmployeeRepository employeeRepository, IGradeRepository gradeRepository,
        IProfileRepository profileRepository,
        IRoleRepository roleRepository,
        ITokenHandler tokenHandler, ApplicationDbContext context)
    {
        _accountRepository = accountRepository;
        _accountRoleRepository = accountRoleRepository;
        _emailHandler = emailHandler;
        _employeeRepository = employeeRepository;
        _gradeRepository = gradeRepository;
        _profileRepository = profileRepository;
        _roleRepository = roleRepository;
        _tokenHandler = tokenHandler;
        _context = context;
    }

    public IEnumerable<AccountDtoGet> Get()
    {
        var accounts = _accountRepository.GetAll().ToList();
        
        return !accounts.Any() ? Enumerable.Empty<AccountDtoGet>() : accounts.Cast<AccountDtoGet>().ToList();
    }

    public AccountDtoGet? Get(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null) return null!;

        return (AccountDtoGet)account;
    }

    public AccountDtoCreate? Create(AccountDtoCreate accountDtoCreate)
    {
        var accountCreated = _accountRepository.Create(accountDtoCreate);
        
        if (accountCreated is null) return null!;

        return (AccountDtoCreate)accountCreated;
    }

    public int Update(AccountDtoUpdate accountDtoUpdate)
    {
        var account = _accountRepository.GetByGuid(accountDtoUpdate.Id);
        
        if (account is null) return -1;

        var accountUpdated = _accountRepository.Update(accountDtoUpdate);
        
        return accountUpdated ? 1 : 0;
    }

    public int Delete(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        
        if (account is null) return -1;

        var accountDeleted = _accountRepository.Delete(account);
        
        return accountDeleted ? 1 : 0;
    }


    public int ChangePassword(AccountDtoChangePassword accountDtoChangePassword)
    {
        var employee = _employeeRepository.GetByEmail(accountDtoChangePassword.Email);
        
        if (employee is null)
            return 0;

        var account = _accountRepository.GetByGuid(employee.Id);
        
        if (account is null)
            return 0;

        if (account.IsUsed)
            return -1;

        if (account.OTP != accountDtoChangePassword.Otp)
            return -2;

        if (account.ExpiredTime < DateTime.Now)
            return -3;

        var isUpdated = _accountRepository.Update(new Account
        {
            Id = account.Id,
            Password = HashingHandler.HashPassword(accountDtoChangePassword.NewPassword),
            IsDeleted = account.IsDeleted,
            OTP = account.OTP,
            ExpiredTime = account.ExpiredTime,
            IsUsed = true,
            CreatedAt = account.CreatedAt,
            LastModifiedAt = DateTime.UtcNow
        });

        return isUpdated ? 1 : -4;
    }

    public string Login(AccountDtoLogin accountDtoLogin)
    {
        var employee = _employeeRepository.GetByEmail(accountDtoLogin.Email);
        
        if (employee is null) return "0";
        
        var profile = _profileRepository.GetByGuid(employee.ProfileId);

        var account = _accountRepository.GetByGuid(employee.Id);
        
        if (account is null) return "0";

        if (!HashingHandler.ValidatePassword(accountDtoLogin.Password, account!.Password)) return "-1";

        try
        {
            var claims = new List<Claim>()
            {
                new("Guid", employee.Id.ToString()),
                new("FullName", $"{employee.FirstName} {employee.LastName}"),
                new("PhotoProfile", profile!.ProfileImage),
                new("GradeGuid", employee.GradeId.ToString() ?? Guid.Empty.ToString()),
                new("ProfileGuid", employee.ProfileId.ToString() ?? Guid.Empty.ToString()),
            };

            var accountRoles = _accountRoleRepository.GetAccountRolesByAccountGuid(account.Id);
            
            var getRolesNameByAccountRole = from accountRole in accountRoles
                                            join role in _roleRepository.GetAll() 
                                                on accountRole.Id equals role.Id
                                            select role.Name;
            
            claims.AddRange(getRolesNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = _tokenHandler.GenerateToken(claims);
            
            return token;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "-2";
        }
    }

    public int ForgotPassword(AccountDtoForgotPassword accountDtoForgotPassword)
    {
        var employee = _employeeRepository.GetByEmail(accountDtoForgotPassword.Email);
        
        if (employee is null)
            return 0;

        var account = _accountRepository.GetByGuid(employee.Id);
        
        if (account is null)
            return -1;

        var otp = new Random().Next(111111, 999999);
        
        var isUpdated = _accountRepository.Update(new Account
        {
            Id = account.Id,
            Password = account.Password,
            IsDeleted = account.IsDeleted,
            OTP = otp,
            ExpiredTime = DateTime.Now.AddMinutes(5),
            IsUsed = false,
            CreatedAt = account.CreatedAt,
            LastModifiedAt = DateTime.UtcNow
        });

        if (!isUpdated)
            return -1;

        _emailHandler.SendEmail(accountDtoForgotPassword.Email,
            "Forgot Password",
            $"Your OTP is {otp}");

        return 1;
    }

    public bool Register(AccountDtoRegister accountDtoRegister)
    {
        using var transaction = _context.Database.BeginTransaction();
        
        try
        {
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

            var profile = new Profile
            {
                Id = Guid.NewGuid(),
                ProfileImage = "",
                Skills = "",
                LinkedIn = "",
                ResumeURL = "",
                CreatedAt = DateTime.UtcNow,
            };
            
            var profileCreated = _profileRepository.Create(profile);

            var employee = new Employee
            {
                Id = new Guid(),
                FirstName = accountDtoRegister.FirstName,
                LastName = accountDtoRegister.LastName ?? "",
                DateOfBirth = accountDtoRegister.BirthDate,
                Gender = accountDtoRegister.Gender,
                HiredDate = accountDtoRegister.HiringDate,
                Email = accountDtoRegister.Email,
                PhoneNumber = accountDtoRegister.PhoneNumber,
                ProfileId = profileCreated.Id,
                GradeId = gradeCreated.Id,
                CreatedAt = DateTime.Now,
                Code = GenerateHandler.GenerateNik(_employeeRepository.GetLastEmployeeCode())
            };

            _employeeRepository.Create(employee);

            var account = new Account
            {
                Id = employee.Id,
                Password = HashingHandler.HashPassword(accountDtoRegister.Password),
                IsDeleted = false,
                IsUsed = false,
                OTP = 0,
                CreatedAt = DateTime.Now,
                ExpiredTime = DateTime.Now.AddMinutes(10)
            };
            
            _accountRepository.Create(account);

            var roleEmployee = _roleRepository.GetByName("Employee");
            
            _accountRoleRepository.Create(new AccountRole
            {
                AccountId = account.Id,
                RoleId = roleEmployee!.Id
            });

            transaction.Commit();
            
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }
}
