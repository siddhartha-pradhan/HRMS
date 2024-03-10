using System.Security.Claims;

namespace HRMS.Application.Interfaces.Contract;

public interface ITokenHandler
{
    string GenerateToken(IEnumerable<Claim> claims);
}
