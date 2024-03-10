namespace HRMS.Application.DTOs.Dashboards;

public class DashboardDtoGetClient
{
    public Guid DepartmentId { get; set; }
    
    public string Name { get; set; }
    
    public int TotalEmployees { get; set; }
}
