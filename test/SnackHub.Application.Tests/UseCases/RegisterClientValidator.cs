using SnackHub.Application.Models;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Tests.UseCases
{
    public class RegisterClientValidator
    {
        public RegisterClientValidator()
        {
        }

        public bool IsValid(RegisterClientRequest registerClientRequest, out RegisterClientResponse response)
        {
            var cpf = new CPF(registerClientRequest.CPF);

            if (!cpf.IsValid())
            {
                return response;
            }

            return true;
        }
    }
}