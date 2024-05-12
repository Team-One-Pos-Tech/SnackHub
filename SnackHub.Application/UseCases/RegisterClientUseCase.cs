using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.UseCases
{
    public class RegisterClientUseCase(IClientRepository clientRepository)
    {
        public RegisterClientResponse Execute(RegisterClientRequest registerClientRequest)
        {
            var response = new RegisterClientResponse(IsValid: true);

            if (!CPF.IsValid(registerClientRequest.CPF))
            {
                response.IsValid = false;
                return response;
            }

            var client = CreateClient(registerClientRequest);

            clientRepository.Add(client);

            return response;
        }

        private static Client CreateClient(RegisterClientRequest registerClientRequest)
        {
            return new Client(
                registerClientRequest.Name, 
                new CPF(registerClientRequest.CPF)
            );
        }
    }
}