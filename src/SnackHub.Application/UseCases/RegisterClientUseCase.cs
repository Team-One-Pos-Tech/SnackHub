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

            var response = new RegisterClientResponse();

            if (!cpf.IsValid())
            {
                response.AddNotification("CPF", "CPF is invalid");
                return response;
            }

            var client = CreateClient(registerClientRequest);

            clientRepository.Add(client);

            return new RegisterClientResponse
            {
                Id = client.Id,
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