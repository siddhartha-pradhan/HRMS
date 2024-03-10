using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Profiles;

public class ProfileDtoGet
{
    public Guid Id { get; set; }
    public string ProfileImage { get; set; }
    public string Skills { get; set; }
    public string LinkedIn { get; set; }
    public string ResumeURL { get; set; }
    
    public static implicit operator Profile(ProfileDtoGet profileDtoGet)
    {
        return new Profile
        {
            Id = profileDtoGet.Id,
            ProfileImage = profileDtoGet.ProfileImage,
            Skills = profileDtoGet.Skills,
            LinkedIn = profileDtoGet.LinkedIn,
            ResumeURL = profileDtoGet.ResumeURL
        };
    }
    
    public static explicit operator ProfileDtoGet(Profile profile)
    {
        return new ProfileDtoGet
        {
            Id = profile.Id,
            ProfileImage = profile.ProfileImage,
            Skills = profile.Skills,
            LinkedIn = profile.LinkedIn,
            ResumeURL = profile.ResumeURL
        };
    }
}
