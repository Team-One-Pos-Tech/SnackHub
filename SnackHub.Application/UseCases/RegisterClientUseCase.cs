using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.UseCases
{
    public class RegisterClientUseCase(IClientRepository clientRepository) : IRegisterClientUseCase
    {
        public RegisterClientResponse Execute(RegisterClientRequest registerClientRequest)
        {
            var response = new RegisterClientResponse(IsValid: true);

            var client = CreateClient(registerClientRequest);

            if (!client.CPF.IsValid())
            {
                response.IsValid = false;
                return response;
            }

            clientRepository.Add(client);

            return response;
        }

        private static Client CreateClient(RegisterClientRequest registerClientRequest)
        {
            return new Client(
                Guid.NewGuid(),
                registerClientRequest.Name,
                new CPF(registerClientRequest.CPF)
            );
        }
    }
}