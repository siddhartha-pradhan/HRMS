﻿using System.Net;
using HRMS.Domain.Constants;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.DTOs.Projects;
using Microsoft.AspNetCore.Authorization;
using HRMS.Infrastructure.Implementation.Services;

namespace HRMS.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;

    public ProjectController(ProjectService projectService)
    {
        _projectService = projectService;
    }

    [Authorize(Roles = $"{nameof(Roles.Manager)}, {nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpGet]
    public IActionResult Get()
    {
        var projects = _projectService.Get();

        if (!projects.Any())
        {
            return NotFound(new ResponseHandler<ProjectDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No projects found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<ProjectDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Projects found",
            Data = projects
        });
    }

    [Authorize(Roles =
        $"{nameof(Roles.Manager)}, {nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var project = _projectService.Get(guid);

        if (project is null)
        {
            return NotFound(new ResponseHandler<ProjectDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Project not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<ProjectDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Project found",
            Data = project
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Manager)}, {nameof(Roles.Admin)}")]
    [HttpPost]
    public IActionResult Create(ProjectDtoCreate projectDtoCreate)
    {
        var projectCreated = _projectService.Create(projectDtoCreate);

        if (projectCreated is null)
        {
            return BadRequest(new ResponseHandler<ProjectDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Project not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<ProjectDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Project created",
            Data = projectCreated
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Manager)}, {nameof(Roles.Admin)}")]
    [HttpPut]
    public IActionResult Update(ProjectDtoUpdate projectDtoUpdate)
    {
        var projectUpdated = _projectService.Update(projectDtoUpdate);

        if (projectUpdated == -1)
        {
            return NotFound(new ResponseHandler<ProjectDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Project not found",
                Data = null
            });
        }

        if (projectUpdated == 0)
        {
            return BadRequest(new ResponseHandler<ProjectDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Project not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<ProjectDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Project updated",
            Data = projectDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Manager)}, {nameof(Roles.Admin)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var projectDeleted = _projectService.Delete(guid);

        if (projectDeleted == -1)
        {
            return NotFound(new ResponseHandler<ProjectDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Project not found",
                Data = null
            });
        }

        if (projectDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ProjectDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Project not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<ProjectDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Project deleted",
            Data = null
        });
    }
}
