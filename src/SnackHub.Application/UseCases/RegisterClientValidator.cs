using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.UseCases
{
    public class RegisterClientValidator : IRegisterClientValidator
    {
        public RegisterClientValidator()
        {
        }

        public bool IsValid(RegisterClientRequest registerClientRequest, out RegisterClientResponse response)
        {
            var cpf = new CPF(registerClientRequest.CPF);
            response = new RegisterClientResponse();

            if (!cpf.IsValid())
            {
                response.AddNotification("CPF", "CPF is invalid.");
                return false;
            }

            return true;
        }
    }
}