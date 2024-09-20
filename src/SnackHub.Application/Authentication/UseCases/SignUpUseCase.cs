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
        private ISignUpFunctionGateway @object;

        public SignUpUseCase(ISignUpFunctionGateway @object)
        {
            this.@object = @object;
        }

        public Task Execute(SignUpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
