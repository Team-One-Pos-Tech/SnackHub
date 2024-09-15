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
        private ISignInFunctionGateway object1;
        private IRegisterClientUseCase object2;

        public SignInUseCase(ISignInFunctionGateway object1, IRegisterClientUseCase object2)
        {
            this.object1 = object1;
            this.object2 = object2;
        }

        public Task<SignInResponse> Execute(SignInRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
