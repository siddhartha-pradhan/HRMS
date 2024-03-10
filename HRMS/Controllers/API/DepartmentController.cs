using System.Net;
using HRMS.Domain.Constants;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using API.DataTransferObjects.Companies;
using Microsoft.AspNetCore.Authorization;
using HRMS.Infrastructure.Implementation.Services;

namespace HRMS.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly DepartmentService _departmentService;

    public DepartmentController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    [Authorize(Roles = $"{nameof(Roles.HR)}, {nameof(Roles.Admin)}, {nameof(Roles.Manager)}, {nameof(Roles.Trainer)}, {nameof(Roles.Employee)}")]
    public IActionResult Get()
    {
        var companies = _departmentService.Get();

        if (!companies.Any())
        {
            return NotFound(new ResponseHandler<DepartmentDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No companies found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<DepartmentDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Companies found",
            Data = companies
        });
    }

    [HttpGet("{guid}")]
    [Authorize(Roles = $"{nameof(Roles.Employee)}, {nameof(Roles.Admin)}, {nameof(Roles.HR)}")]
    public IActionResult Get(Guid guid)
    {
        var company = _departmentService.Get(guid);

        if (company is null)
        {
            return NotFound(new ResponseHandler<DepartmentDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Company not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<DepartmentDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Company found",
            Data = company
        });
    }

    [Authorize(Roles = $"{nameof(Roles.HR)}, {nameof(Roles.Admin)}")]
    [HttpPost]
    public IActionResult Create(DepartmentDtoCreate companyDtoCreate)
    {
        var companyCreated = _departmentService.Create(companyDtoCreate);

        if (companyCreated is null)
        {
            return BadRequest(new ResponseHandler<DepartmentDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Company not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<DepartmentDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Company created",
            Data = companyCreated
        });
    }

    [Authorize(Roles = $"{nameof(Roles.HR)}, {nameof(Roles.Admin)}")]
    [HttpPut]
    public IActionResult Update(DepartmentDtoUpdate companyDtoUpdate)
    {
        var companyUpdated = _departmentService.Update(companyDtoUpdate);

        if (companyUpdated == -1)
        {
            return NotFound(new ResponseHandler<DepartmentDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Company not found",
                Data = null
            });
        }

        if (companyUpdated == 0)
        {
            return BadRequest(new ResponseHandler<DepartmentDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Company not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<DepartmentDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Company updated",
            Data = companyDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(Roles.HR)}, {nameof(Roles.Admin)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var companyDeleted = _departmentService.Delete(guid);

        if (companyDeleted == -1)
        {
            return NotFound(new ResponseHandler<DepartmentDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Company not found",
                Data = null
            });
        }

        if (companyDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<DepartmentDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Company not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<DepartmentDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Company deleted",
            Data = null
        });
    }
}
