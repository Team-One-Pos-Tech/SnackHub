using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;

namespace SnackHub.Controllers;

public class LoginViewModel
{
    public string Cpf { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IConfiguration Configuration, ISignUpFunctionGateway signUpFunctionGateway, ISignInFunctionGateway signInFunctionGateway) : ControllerBase
{
    [HttpPost, Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterClientRequest user)
    {
        var defaultPassword = Environment.GetEnvironmentVariable("DEFAULT_USERS_PASSWORD");
        
        var signUpRequest = new SignUpRequest()
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
    
    [HttpPost, Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginViewModel user)
    {
        var defaultPassword = Environment.GetEnvironmentVariable("DEFAULT_USERS_PASSWORD");
        
        var signInRequest = new SignInRequest()
        {
            Username = user.Cpf,
            Password = defaultPassword!
        };

        try
        {
            var authResponse = await signInFunctionGateway.Execute(signInRequest);
            return Ok(authResponse);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}