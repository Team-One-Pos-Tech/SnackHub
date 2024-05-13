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
            var cpf = new CPF(registerClientRequest.CPF);

            if (!cpf.IsValid())
            {
                return new RegisterClientResponse(isValid: false);
            }

            var client = CreateClient(registerClientRequest);

            clientRepository.Add(client);

            var response = new RegisterClientResponse(isValid: true);
            response.Id = client.Id;
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