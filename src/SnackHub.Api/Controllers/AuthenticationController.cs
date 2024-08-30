using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using SnackHub.Extensions;
using SnackHub.Models;

namespace SnackHub.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IConfiguration Configuration, 
    ISignUpFunctionGateway signUpFunctionGateway, 
    ISignInFunctionGateway signInFunctionGateway, 
    IRegisterClientUseCase registerClientUseCase) : ControllerBase
{
    [HttpPost, Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterClientRequest user)
    {
        var defaultPassword = Environment.GetEnvironmentVariable("DEFAULT_USERS_PASSWORD");
        
        var request = new RegisterClientRequest(user.Name, user.CPF);

        var response = await registerClientUseCase.Execute(request);
        
        if (!response.IsValid) 
        {
            return ValidationProblem(ModelState.AddNotifications(response.Notifications));
        }

        try
        {
            var signUpRequest = new SignUpRequest()
            {
                Name = user.Name,
                Cpf = user.CPF,
                Password = defaultPassword!
            };
            
            await signUpFunctionGateway.Execute(signUpRequest);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginModel user)
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
            
            return Ok(authResponse.AuthenticationResult.IdToken);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}