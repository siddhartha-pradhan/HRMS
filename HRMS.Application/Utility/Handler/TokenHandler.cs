using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using HRMS.Application.Interfaces.Contract;

namespace HRMS.Application.Utility.Handler;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;
    
    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTService:Key"]));
        
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        
        var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWTService:Issuer"],
            audience: _configuration["JWTService:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWTService:DurationInMinutes"])),
            signingCredentials: signinCredentials);
        
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return tokenString;
    }
}
