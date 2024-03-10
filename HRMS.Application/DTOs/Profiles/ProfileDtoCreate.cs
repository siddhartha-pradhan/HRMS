using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Profiles;

public class ProfileDtoCreate
{
    public string ProfileImage { get; set; }
    public string Skills { get; set; }
    public string Linkedin { get; set; }
    public string ResumeURL { get; set; }
    
    public static implicit operator Profile(ProfileDtoCreate profileDtoCreate)
    {
        return new Profile
        {
            Skills = profileDtoCreate.Skills,
            ProfileImage = profileDtoCreate.ProfileImage,
            LinkedIn = profileDtoCreate.Linkedin,
            ResumeURL = profileDtoCreate.ResumeURL,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static explicit operator ProfileDtoCreate(Profile profile)
    {
        return new ProfileDtoCreate
        {
            Skills = profile.Skills,
            ProfileImage = profile.ProfileImage,
            Linkedin = profile.LinkedIn,
            ResumeURL = profile.ResumeURL
        };
    }
}
