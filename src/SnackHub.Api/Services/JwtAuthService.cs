using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using SnackHub.Domain.Models.Gateways.Models;

namespace SnackHub.Services;

public class JwtAuthService(IConfiguration configuration): IAuthService
{
    public Task<AuthResponseType> Execute(SignInRequest request)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
                        
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokeOptions = new JwtSecurityToken(
            issuer : issuer,
            audience: audience,
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(2),
            signingCredentials: signinCredentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return Task.FromResult(new AuthResponseType(tokenString, true));
    }
}