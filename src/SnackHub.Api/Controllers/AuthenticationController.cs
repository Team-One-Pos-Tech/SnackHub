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

/// <summary>
/// Handles user authentication.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(
    ISignInUseCase signInUseCase,
    ISignUpUseCase signUpUseCase) : ControllerBase
{
    private const string DefaultUsersPassword = "Default-password-99!";

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Action result indicating the outcome of the registration.</returns>
    [HttpPost, Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterClientRequest user)
    {
        var signUpRequest = new SignUpRequest(
            user.Name,
            user.CPF,
            DefaultUsersPassword,
            user.Email
        );

        var response = await signUpUseCase.Execute(signUpRequest);
        
        if (!response.IsValid) 
        {
            return ValidationProblem(ModelState.AddNotifications(response.Notifications));
        }

        return Ok(response);
    }

    /// <summary>
    /// Authenticates a user.
    /// </summary>
    /// <param name="user">To sign in as an Anonymous User the CPF value should be empty</param>
    /// <returns>Action result indicating the outcome of the authentication</returns>
    [HttpPost, Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginModel user)
    {
        var signInRequest = new SignInRequest(user.Cpf, DefaultUsersPassword);

        var response = await signInUseCase.Execute(signInRequest);

        return Ok(response);
    }
}