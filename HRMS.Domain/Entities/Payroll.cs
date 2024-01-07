using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Payroll : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }
    
    public int Month { get; set; }
    
    public int Year { get; set; }
    
    public DateTime IssuedDate { get; set; }
    
    public decimal BasicSalary { get; set; }
    
    public decimal Allowance { get; set; }
    
    public decimal OverTime { get; set; }
    
    public decimal Incentives { get; set; }
    
    public decimal Bonus { get; set; }
    
    public decimal Tax { get; set; }
    
    public decimal ProvidentFund { get; set; }
    
    public decimal OtherDeductions { get; set; }
    
    public virtual Employee Employee { get; set; }
}