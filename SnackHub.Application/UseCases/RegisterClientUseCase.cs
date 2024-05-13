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
                return new RegisterClientResponse { IsValid = false };

            var client = CreateClient(registerClientRequest);

            clientRepository.Add(client);

            return CreateResponse(client);
        }

        private static RegisterClientResponse CreateResponse(Client client)
        {
            return new RegisterClientResponse 
            {
                Id = client.Id,
                IsValid = true
            };
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