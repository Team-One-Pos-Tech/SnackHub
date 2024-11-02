using SnackHub.Application.Authentication.Contracts;
using SnackHub.Application.Authentication.Models;
using SnackHub.Application.Client.Contracts;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;

namespace SnackHub.Application.Authentication.UseCases;

public class SignInUseCase(IAuthService auth, IGetClientUseCase getClientUseCase) : ISignInUseCase
{
    private const string AnonymousUsername = "00000000000";

    public async Task<SignInResponse> Execute(SignInRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username))
        {
            request.Username = AnonymousUsername;
        }
        
        var client = await getClientUseCase.Execute(request.Username);

        if (client == null)
        {
            var signInResponse = new SignInResponse(null);
            signInResponse.AddNotification("User", "User not found");
            return signInResponse;
        }

        var response = await auth.Execute(request);

        return new SignInResponse(response.IdToken);
    }
}