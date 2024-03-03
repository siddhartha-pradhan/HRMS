using System.Net;

namespace HRMS.Application.DTOs.Base;

public class ResponseDto<TEntity>
{
    public HttpStatusCode Code { get; set; }
    
    public string Status { get; set; }
    
    public string Message { get; set; }
    
    public TEntity? Data { get; set; }
}