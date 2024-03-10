using HRMS.Application.DTOs.Profiles;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Services;

public class ProfileService
{
    private readonly IProfileRepository _profileRepository;

    public ProfileService(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public IEnumerable<ProfileDtoGet> Get()
    {
        var profiles = _profileRepository.GetAll().ToList();
        
        return !profiles.Any() ? Enumerable.Empty<ProfileDtoGet>() : profiles.Cast<ProfileDtoGet>().ToList();
    }

    public ProfileDtoGet? Get(Guid guid)
    {
        var profile = _profileRepository.GetByGuid(guid);
        
        if (profile is null) return null!;

        return (ProfileDtoGet)profile;
    }

    public ProfileDtoCreate? Create(ProfileDtoCreate profileDtoCreate)
    {
        var profileCreated = _profileRepository.Create(profileDtoCreate);
        
        if (profileCreated is null) return null!;

        return (ProfileDtoCreate)profileCreated;
    }

    public int Update(ProfileDtoUpdate profileDtoUpdate)
    {
        var profile = _profileRepository.GetByGuid(profileDtoUpdate.Id);
        
        if (profile is null) return -1;

        var profileUpdated = _profileRepository.Update(profileDtoUpdate);
        
        return !profileUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var profile = _profileRepository.GetByGuid(guid);
        
        if (profile is null) return -1;

        var profileDeleted = _profileRepository.Delete(profile);
        
        return !profileDeleted ? 0 : 1;
    }
}
