using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Interview : BaseEntity<Guid>
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string Link { get; set; }
    
    public DateTime InterviewDate { get; set; }
}