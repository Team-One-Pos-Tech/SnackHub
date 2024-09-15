using SnackHub.Application.Authentication.Models;
using SnackHub.Application.Client.Contracts;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackHub.Application.Authentication.UseCases
{
    public class SignInUseCase
    {
        private readonly ISignInFunctionGateway _signInFunction;

        public SignInUseCase(ISignInFunctionGateway signInFunction)
        {
            this._signInFunction = signInFunction;
        }

        public async Task<SignInResponse> Execute(SignInRequest request)
        {
            var response = await _signInFunction.Execute(request);

            return new SignInResponse(response.IdToken);
        }
    }
}
