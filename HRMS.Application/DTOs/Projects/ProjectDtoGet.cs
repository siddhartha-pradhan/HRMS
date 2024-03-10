using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Projects;

public class ProjectDtoGet
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lead { get; set; }
    public string Description { get; set; }

    public static implicit operator Project(ProjectDtoGet projectDtoGet)
    {
        return new Project
        {
            Id = projectDtoGet.Id,
            Name = projectDtoGet.Name,
            Lead = projectDtoGet.Lead,
            Description = projectDtoGet.Description
        };
    }

    public static explicit operator ProjectDtoGet(Project project)
    {
        return new ProjectDtoGet
        {
            Id = project.Id,
            Name = project.Name,
            Lead = project.Lead,
            Description = project.Description
        };
    }
}
