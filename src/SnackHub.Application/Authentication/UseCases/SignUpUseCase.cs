using SnackHub.Application.Authentication.Contracts;
using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;

namespace SnackHub.Application.Authentication.UseCases
{
    public class SignUpUseCase(IRegisterClientUseCase registerClient) : ISignUpUseCase
    {
        public async Task<RegisterClientResponse> Execute(SignUpRequest request)
        {
            var response = await registerClient.Execute(
                new RegisterClientRequest(request.Name, request.Username)
                {
                    Email = request.Email
                });
            
            return response;
        }
    }
}
