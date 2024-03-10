using System.Net;
using HRMS.Domain.Constants;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HRMS.Application.DTOs.EmployeeProjects;
using HRMS.Infrastructure.Implementation.Services;

namespace HRMS.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class EmployeeProjectController : ControllerBase
{
    private readonly EmployeeProjectService _employeeProjectService;

    public EmployeeProjectController(EmployeeProjectService employeeProjectService)
    {
        _employeeProjectService = employeeProjectService;
    }

    [Authorize(Roles = $"{nameof(Roles.Manager)}, {nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpGet]
    public IActionResult Get()
    {
        var employeeProjects = _employeeProjectService.Get();

        if (!employeeProjects.Any())
        {
            return NotFound(new ResponseHandler<EmployeeProjectDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employee projects found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeProjectDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee projects found",
            Data = employeeProjects
        });
    }

    [Authorize(Roles =
        $"{nameof(Roles.Manager)}, {nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var employeeProject = _employeeProjectService.Get(guid);

        if (employeeProject is null)
        {
            return NotFound(new ResponseHandler<EmployeeProjectDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee project not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeProjectDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee project found",
            Data = employeeProject
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpPost]
    public IActionResult Create(EmployeeProjectDtoCreate employeeProjectDtoCreate)
    {
        var employeeProjectCreated = _employeeProjectService.Create(employeeProjectDtoCreate);

        if (employeeProjectCreated is null)
        {
            return BadRequest(new ResponseHandler<EmployeeProjectDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee project not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeProjectDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Employee project created",
            Data = employeeProjectCreated
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Trainer)}, {nameof(Roles.Manager)}, {nameof(Roles.Admin)}")]
    [HttpPut]
    public IActionResult Update(EmployeeProjectDtoUpdate employeeProjectDtoUpdate)
    {
        var employeeProjectUpdated = _employeeProjectService.Update(employeeProjectDtoUpdate);

        if (employeeProjectUpdated == -1)
        {
            return NotFound(new ResponseHandler<EmployeeProjectDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee project not found",
                Data = null
            });
        }

        if (employeeProjectUpdated == 0)
        {
            return BadRequest(new ResponseHandler<EmployeeProjectDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee project not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeProjectDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee project updated",
            Data = employeeProjectDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var employeeProjectDeleted = _employeeProjectService.Delete(guid);

        if (employeeProjectDeleted == -1)
        {
            return NotFound(new ResponseHandler<EmployeeProjectDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee project not found",
                Data = null
            });
        }

        if (employeeProjectDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<EmployeeProjectDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Employee Project not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeProjectDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee project deleted",
            Data = null
        });
    }
    
    
    [HttpGet("GetByProject/{guid}")]
    public IActionResult GetByProject(Guid guid)
    {
        var projects = _employeeProjectService.GetByProject(guid);

        if (!projects.Any())
        {
            return NotFound(new ResponseHandler<EmployeeProjectDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No projects found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeProjectDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Projects found",
            Data = projects
        });
    }
}
