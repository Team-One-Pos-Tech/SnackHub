using SnackHub.Application.Authentication.Contracts;
using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;

namespace SnackHub.Application.Authentication.UseCases
{
    public class SignUpUseCase(ISignUpFunctionGateway signUpFunctionGateway, IRegisterClientUseCase registerClient) : ISignUpUseCase
    {
        private readonly ISignUpFunctionGateway _signUpFunctionGateway = signUpFunctionGateway;

        public async Task<RegisterClientResponse> Execute(SignUpRequest request)
        {
            var response = await registerClient.Execute(new RegisterClientRequest(request.Name, request.Username));

            if (!response.IsValid)
            {
                return response;
            }

            await _signUpFunctionGateway.Execute(request);

            return response;
        }
    }
}
