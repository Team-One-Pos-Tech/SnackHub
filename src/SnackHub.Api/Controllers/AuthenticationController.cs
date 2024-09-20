using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnackHub.Application.Authentication.Contracts;
using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Configuration;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using SnackHub.Extensions;
using SnackHub.Models;

namespace SnackHub.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(
    ISignUpFunctionGateway signUpFunctionGateway, 
    ISignInUseCase signInUseCase, 
    IRegisterClientUseCase registerClientUseCase) : ControllerBase
{
    private const string DefaultUsersPassword = "Default-password-99!";

    [HttpPost, Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterClientRequest user)
    {
        var request = new RegisterClientRequest(user.Name, user.CPF);

        var response = await registerClientUseCase.Execute(request);
        
        if (!response.IsValid) 
        {
            return ValidationProblem(ModelState.AddNotifications(response.Notifications));
        }

        try
        {
            await RegisterOnIdentityProvider(user);

            return Ok("User registered successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private async Task RegisterOnIdentityProvider(RegisterClientRequest user)
    {
        var signUpRequest = new SignUpRequest(
            user.Name,
            user.CPF,
            DefaultUsersPassword,
            user.Email
        );

        await signUpFunctionGateway.Execute(signUpRequest);
    }

    [HttpPost, Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginModel user)
    {
        var signInRequest = new SignInRequest(user.Cpf, DefaultUsersPassword);

        var response = await signInUseCase.Execute(signInRequest);

        return Ok(response);
    }
}