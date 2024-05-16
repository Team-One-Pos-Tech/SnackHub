using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Client.UseCases
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