using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models;

namespace SnackHub.Controllers;

public class LoginViewModel
{
    public string Cpf { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IConfiguration Configuration, ISignUpFunctionGateway signUpFunctionGateway) : ControllerBase
{
    // [HttpPost, Route("signin")]
    // public IActionResult SignIn([FromBody] LoginViewModel user)
    // {
    //     if (user == null)
    //     {
    //         return BadRequest("Request do cliente inválido");
    //     }
    //
    //     if (user.Cpf == "40481414061")
    //     {
    //         var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
    //         var _issuer = Configuration["Jwt:Issuer"];
    //         var _audience = Configuration["Jwt:Audience"];
    //
    //         var signinCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);
    //
    //         var tokeOptions = new JwtSecurityToken(
    //             issuer: _issuer,
    //             audience: _audience,
    //             claims: new List<Claim>(),
    //             expires: DateTime.Now.AddMinutes(20),
    //             signingCredentials: signinCredentials);
    //
    //         var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    //
    //         return Ok(tokenString);
    //     }
    //     else
    //     {
    //         return Unauthorized();
    //     }
    // }
    
    
    [HttpPost, Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterClientRequest user)
    {
        var defaultPassword = Environment.GetEnvironmentVariable("DEFAULT_USERS_PASSWORD");
        
        var signUpRequest = new SignUpRequest
        {
            Name = user.Name,
            Cpf = user.CPF,
            Password = defaultPassword!
        };

        try
        {
            await signUpFunctionGateway.Execute(signUpRequest);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }
}