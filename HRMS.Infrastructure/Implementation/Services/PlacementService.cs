using HRMS.Application.DTOs.Placements;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Services;

public class PlacementService
{
    private readonly IPlacementRepository _placementRepository;

    public PlacementService(IPlacementRepository placementRepository)
    {
        _placementRepository = placementRepository;
    }

    public IEnumerable<PlacementDtoGet> Get()
    {
        var placements = _placementRepository.GetAll().ToList();
        
        return !placements.Any() ? Enumerable.Empty<PlacementDtoGet>() : placements.Cast<PlacementDtoGet>().ToList();
    }

    public PlacementDtoGet? Get(Guid guid)
    {
        var placement = _placementRepository.GetByGuid(guid);
        
        if (placement is null) return null!;

        return (PlacementDtoGet)placement;
    }

    public PlacementDtoCreate? Create(PlacementDtoCreate placementDtoCreate)
    {
        var placementCreated = _placementRepository.Create(placementDtoCreate);
        
        if (placementCreated is null) return null!;

        return (PlacementDtoCreate)placementCreated;
    }

    public int Update(PlacementDtoUpdate placementDtoUpdate)
    {
        var placement = _placementRepository.GetByGuid(placementDtoUpdate.Id);
        
        if (placement is null) return -1;

        var placementUpdated = _placementRepository.Update(placementDtoUpdate);
        
        return !placementUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var placement = _placementRepository.GetByGuid(guid);
        
        if (placement is null) return -1;

        var placementDeleted = _placementRepository.Delete(placement);
        
        return !placementDeleted ? 0 : 1;
    }
}
