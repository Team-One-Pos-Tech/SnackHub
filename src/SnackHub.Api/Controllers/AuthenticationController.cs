using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace SnackHub.Controllers;

public class LoginViewModel
{
    public string Cpf { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IConfiguration Configuration) : ControllerBase
{
    [HttpPost, Route("login")]
    public IActionResult Login([FromBody] LoginViewModel user)
    {
        if (user == null)
        {
            return BadRequest("Request do cliente inválido");
        }

        if (user.Cpf == "40481414061")
        {
            var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var _issuer = Configuration["Jwt:Issuer"];
            var _audience = Configuration["Jwt:Audience"];

            var signinCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(tokenString);
        }
        else
        {
            return Unauthorized();
        }
    }
}