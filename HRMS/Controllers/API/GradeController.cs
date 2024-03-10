﻿using System.Net;
using HRMS.Domain.Constants;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using HRMS.Application.DTOs.Grades;
using Microsoft.AspNetCore.Authorization;
using HRMS.Infrastructure.Implementation.Services;

namespace HRMS.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly GradeService _gradeService;

    public GradeController(GradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [Authorize(Roles = $"{nameof(Roles.Manager)}, {nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpGet]
    public IActionResult Get()
    {
        var grades = _gradeService.Get();

        if (!grades.Any())
        {
            return NotFound(new ResponseHandler<GradeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No grades found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GradeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Grades found",
            Data = grades
        });
    }

    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var grade = _gradeService.Get(guid);

        if (grade is null)
        {
            return NotFound(new ResponseHandler<GradeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Grade not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Grade found",
            Data = grade
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpPost]
    public IActionResult Create(GradeDtoCreate gradeDtoCreate)
    {
        var gradeCreated = _gradeService.Create(gradeDtoCreate);

        if (gradeCreated is null)
        {
            return BadRequest(new ResponseHandler<GradeDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Grade not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Grade created",
            Data = gradeCreated
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpPut]
    public IActionResult Update(GradeDtoUpdate gradeDtoUpdate)
    {
        var gradeUpdated = _gradeService.Update(gradeDtoUpdate);

        if (gradeUpdated == -1)
        {
            return NotFound(new ResponseHandler<GradeDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Grade not found",
                Data = null
            });
        }

        if (gradeUpdated == 0)
        {
            return BadRequest(new ResponseHandler<GradeDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Grade not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Grade updated",
            Data = gradeDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var gradeDeleted = _gradeService.Delete(guid);

        if (gradeDeleted == -1)
        {
            return NotFound(new ResponseHandler<GradeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Grade not found",
                Data = null
            });
        }

        if (gradeDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<GradeDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Grade not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Grade deleted",
            Data = null
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpPost("CreateGenerate")]
    public IActionResult CreateGenerate(GradeDtoGenerateScore gradeDtoGenerateScore)
    {
        var gradeCreated = _gradeService.CreateGenerateScore(gradeDtoGenerateScore);

        if (gradeCreated is null)
        {
            return BadRequest(new ResponseHandler<GradeDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Grade not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Grade created",
            Data = gradeCreated
        });
    }

    [Authorize(Roles = $"{nameof(Roles.Trainer)}, {nameof(Roles.Admin)}")]
    [HttpPut("UpdateGenerate")]
    public IActionResult UpdateGenerate(GradeDtoUpdateGenerateScore gradeDtoUpdateGenerateScore)
    {
        var gradeUpdated = _gradeService.UpdateGenerateScore(gradeDtoUpdateGenerateScore);

        if (gradeUpdated == -1)
        {
            return NotFound(new ResponseHandler<GradeDtoUpdateGenerateScore>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Grade not found",
                Data = null
            });
        }

        if (gradeUpdated == 0)
        {
            return BadRequest(new ResponseHandler<GradeDtoUpdateGenerateScore>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Grade not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoUpdateGenerateScore>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Grade updated",
            Data = gradeDtoUpdateGenerateScore
        });
    }
}
