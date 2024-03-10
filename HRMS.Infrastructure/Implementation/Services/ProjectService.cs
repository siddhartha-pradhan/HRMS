using HRMS.Application.DTOs.Projects;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Services;

public class ProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public IEnumerable<ProjectDtoGet> Get()
    {
        var projects = _projectRepository.GetAll().ToList();
        
        return !projects.Any() ? Enumerable.Empty<ProjectDtoGet>() : projects.Cast<ProjectDtoGet>().ToList();
    }

    public ProjectDtoGet? Get(Guid guid)
    {
        var project = _projectRepository.GetByGuid(guid);
        
        if (project is null) return null;

        return (ProjectDtoGet)project;
    }

    public ProjectDtoCreate Create(ProjectDtoCreate projectDtoCreate)
    {
        var projectCreated = _projectRepository.Create(projectDtoCreate);
        
        if (projectCreated is null) return null!;

        return (ProjectDtoCreate)projectCreated;
    }

    public int Update(ProjectDtoUpdate projectDtoUpdate)
    {
        var project = _projectRepository.GetByGuid(projectDtoUpdate.Id);
        
        if (project is null) return -1;

        var projectUpdated = _projectRepository.Update(projectDtoUpdate);
        
        return !projectUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var project = _projectRepository.GetByGuid(guid);
        
        if (project is null) return -1;

        var projectDeleted = _projectRepository.Delete(project);
        
        return !projectDeleted ? 0 : 1;
    }
}
