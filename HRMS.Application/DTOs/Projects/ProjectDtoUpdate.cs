using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Projects;

public class ProjectDtoUpdate
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lead { get; set; }
    public string Description { get; set; }

    public static implicit operator Project(ProjectDtoUpdate projectDtoUpdate)
    {
        return new Project
        {
            Id = projectDtoUpdate.Id,
            Name = projectDtoUpdate.Name,
            Lead = projectDtoUpdate.Lead,
            Description = projectDtoUpdate.Description,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator ProjectDtoUpdate(Project project)
    {
        return new ProjectDtoUpdate
        {
            Id = project.Id,
            Name = project.Name,
            Lead = project.Lead,
            Description = project.Description
        };
    }
}
