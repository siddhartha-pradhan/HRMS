using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Projects;

public class ProjectDtoCreate
{
    public string Name { get; set; }
    public string Lead { get; set; }
    public string Description { get; set; }

    public static implicit operator Project(ProjectDtoCreate projectDtoCreate)
    {
        return new Project
        {
            Name = projectDtoCreate.Name,
            Lead = projectDtoCreate.Lead,
            Description = projectDtoCreate.Description,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static explicit operator ProjectDtoCreate(Project project)
    {
        return new ProjectDtoCreate
        {
            Name = project.Name,
            Lead = project.Lead,
            Description = project.Description
        };
    }
}
