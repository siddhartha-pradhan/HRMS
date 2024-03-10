using System.Net;
using HRMS.Domain.Constants;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.DTOs.Employees;
using Microsoft.AspNetCore.Authorization;
using HRMS.Infrastructure.Implementation.Services;

namespace HRMS.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [Authorize(Roles = $"{nameof(Constants.Roles.HR)}, {nameof(Roles.Manager)}, {nameof(Roles.Trainer)}, {nameof(Roles.Admin)}, {nameof(Roles.Employee)}")]
    public IActionResult Get()
    {
        var employees = _employeeService.Get();

        if (!employees.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees found",
            Data = employees
        });
    }

    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var employee = _employeeService.Get(guid);

        if (employee is null)
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee found",
            Data = employee
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Employee)}, {nameof(Roles.Admin)}, {nameof(Roles.HR)}")]
    [HttpPost]
    public IActionResult Create(EmployeeDtoCreate employeeDtoCreate)
    {
        var employeeCreated = _employeeService.Create(employeeDtoCreate);

        if (employeeCreated is null)
        {
            return BadRequest(new ResponseHandler<EmployeeDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Employee created",
            Data = employeeCreated
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Employee)}, {nameof(Roles.Admin)}, {nameof(Roles.HR)}, {nameof(Roles.Manager)}, {nameof(Roles.Trainer)}")]
    [HttpPut]
    public IActionResult Update(EmployeeDtoUpdate employeeDtoUpdate)
    {
        var employeeUpdated = _employeeService.Update(employeeDtoUpdate);

        if (employeeUpdated == -1)
        {
            return NotFound(new ResponseHandler<EmployeeDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee not found",
                Data = null
            });
        }

        if (employeeUpdated == 0)
        {
            return BadRequest(new ResponseHandler<EmployeeDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee not updated",
                Data = null
            });
        }


        return Ok(new ResponseHandler<EmployeeDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee updated",
            Data = employeeDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(Roles.HR)}, {nameof(Roles.Admin)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var employeeDeleted = _employeeService.Delete(guid);

        if (employeeDeleted == -1)
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee not found",
                Data = null
            });
        }

        if (employeeDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Employee not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee deleted",
            Data = null
        });
    }

    [Authorize(Roles = $"{nameof(Roles.HR)}, {nameof(Roles.Admin)}, {nameof(Roles.Manager)}, {nameof(Roles.Trainer)}")]
    [HttpGet("GetByRole/{guid}")]
    public IActionResult GetByRole(Guid guid)
    {
        var employees = _employeeService.GetByRole(guid);

        if (!employees.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees found",
            Data = employees
        });
    }

    [Authorize(Roles = $"{nameof(Roles.HR)}, {nameof(Roles.Admin)}")]
    [HttpGet("GetExcludeRole/{guid}")]
    public IActionResult GetExcludeRole(Guid guid)
    {
        var employees = _employeeService.GetExcludeRole(guid);

        if (!employees.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees found",
            Data = employees
        });
    }

    [HttpGet("GetByProject/{guid}")]
    public IActionResult GetByProject(Guid guid)
    {
        var employees = _employeeService.GetEmployeeByProject(guid);

        if (!employees.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees found",
            Data = employees
        });
    }

    [HttpGet("GetExcludeProject/{guid}")]
    public IActionResult GetExcludeProject(Guid guid)
    {
        var employees = _employeeService.GetExcludeProject(guid);

        if (!employees.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees found",
            Data = employees
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Employee)}, {nameof(Roles.Trainer)}, {nameof(Roles.Manager)}, {nameof(Roles.HR)}, {nameof(Roles.Admin)}")]
    [HttpGet("GetByEmail/{email}")]
    public IActionResult GetByEmail(string email)
    {
        var employee = _employeeService.GetByEmail(email);

        if (employee is null)

        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee found",
            Data = employee
        });
    }


    [HttpGet("GetByJob/{guid}")]
    public IActionResult GetByJob(Guid guid)
    {
        var employees = _employeeService.GetEmployeeByJob(guid);

        if (!employees.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees found",
            Data = employees
        });
    }

    [HttpGet("GetExcludeJob/{guid}")]
    public IActionResult GetExcludeJob(Guid guid)
    {
        var employees = _employeeService.GetExcludeJob(guid);

        if (!employees.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees found",
            Data = employees
        });
    }
}
