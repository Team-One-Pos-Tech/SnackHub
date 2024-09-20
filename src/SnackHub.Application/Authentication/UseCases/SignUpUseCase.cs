using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackHub.Application.Authentication.UseCases
{
    public class SignUpUseCase(ISignUpFunctionGateway signUpFunctionGateway, IRegisterClientUseCase registerClient)
    {
        private readonly ISignUpFunctionGateway _signUpFunctionGateway = signUpFunctionGateway;

        public async Task Execute(SignUpRequest request)
        {
            var response = await registerClient.Execute(new RegisterClientRequest(request.Name, request.Cpf));

            await _signUpFunctionGateway.Execute(request);
        }
    }
}
