using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Attendance : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }
    
    public DateTime CheckInDateTime { get; set; }

    public DateTime CheckOutDateTime { get; set; }
    
    public string Remarks { get; set; } // Early, On Time, Late
}