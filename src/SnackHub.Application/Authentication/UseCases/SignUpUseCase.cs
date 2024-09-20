using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackHub.Application.Authentication.UseCases
{
    public class SignUpUseCase
    {
        private readonly ISignUpFunctionGateway _signUpFunctionGateway;

        public SignUpUseCase(ISignUpFunctionGateway signUpFunctionGateway)
        {
            _signUpFunctionGateway = signUpFunctionGateway;
        }

        public async Task Execute(SignUpRequest request)
        {
            await _signUpFunctionGateway.Execute(request);
        }
    }
}
