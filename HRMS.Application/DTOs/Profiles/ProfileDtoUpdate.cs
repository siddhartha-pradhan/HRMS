using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Profiles;

public class ProfileDtoUpdate
{
    public Guid Id { get; set; }
    public string ProfileImage { get; set; }
    public string Skills { get; set; }
    public string LinkedIn { get; set; }
    public string ResumeURL { get; set; }
    
    public static implicit operator Profile(ProfileDtoUpdate profileDtoUpdate)
    {
        return new Profile
        {
            Id = profileDtoUpdate.Id,
            ProfileImage = profileDtoUpdate.ProfileImage,
            Skills = profileDtoUpdate.Skills,
            LinkedIn = profileDtoUpdate.LinkedIn,
            ResumeURL = profileDtoUpdate.ResumeURL,
            LastModifiedAt = DateTime.UtcNow
        };
    }
    
    public static explicit operator ProfileDtoUpdate(Profile profile)
    {
        return new ProfileDtoUpdate
        {
            Id = profile.Id,
            ProfileImage = profile.ProfileImage,
            Skills = profile.Skills,
            LinkedIn = profile.LinkedIn,
            ResumeURL = profile.ResumeURL
        };
    }
}
