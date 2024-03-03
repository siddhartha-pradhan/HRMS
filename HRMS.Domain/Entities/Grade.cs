using HRMS.Domain.Base;
using GradeLevel = HRMS.Domain.Constants.Grade;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities;

public class Grade : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }
    
    public GradeLevel GradeLevel { get; set; }
    
    public int FirstScoreSegment { get; set; }
    
    public int SecondScoreSegment { get; set; }
    
    public int ThirdScoreSegment { get; set; }
    
    public int FourthScoreSegment { get; set; }
    
    public double TotalScore { get; set; }
    
    [ForeignKey("EmployeeId")]
    public virtual Employee? Employee { get; set; }
}