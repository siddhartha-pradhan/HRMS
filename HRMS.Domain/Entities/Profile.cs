using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Profile : BaseEntity<Guid>
{
    public string ProfileImage { get; set; }
    
    public string Skills { get; set; }
    
    public string LinkedIn { get; set; }
    
    public string ResumtURL { get; set; }
}