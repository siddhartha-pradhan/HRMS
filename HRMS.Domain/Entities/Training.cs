using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Training : BaseEntity<Guid>
{
    public string Title { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int MaxAttendees { get; set; }
}