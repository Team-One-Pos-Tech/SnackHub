using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Client.UseCases
{
    public class RegisterClientUseCase(IClientRepository clientRepository) : IRegisterClientUseCase
    {
        public async Task<RegisterClientResponse> Execute(RegisterClientRequest registerClientRequest)
        {
            var cpf = new CPF(registerClientRequest.CPF);

            if (!cpf.IsValid())
                return new RegisterClientResponse { IsValid = false };

            var client = CreateClient(registerClientRequest);

            await clientRepository.AddAsync(client);
            return CreateResponse(client);
        }

        private static RegisterClientResponse CreateResponse(Domain.Entities.Client client)
        {
            return new RegisterClientResponse 
            {
                Id = client.Id,
                IsValid = true
            };
        }

        private static Domain.Entities.Client CreateClient(RegisterClientRequest registerClientRequest)
        {
            return new Domain.Entities.Client(
                Guid.NewGuid(),
                registerClientRequest.Name,
                new CPF(registerClientRequest.CPF)
            );
        }
    }
}