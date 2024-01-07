using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Review : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }
    
    public DateTime ReviewDate { get; set; }
    
    public decimal ReviewScore { get; set; }
    
    public bool IsReviewApproved { get; set; }
}