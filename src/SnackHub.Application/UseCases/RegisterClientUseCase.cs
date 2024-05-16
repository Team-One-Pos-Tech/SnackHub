using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.UseCases
{
    public class RegisterClientUseCase(IClientRepository clientRepository, IRegisterClientValidator validator) : IRegisterClientUseCase
    {
        public RegisterClientResponse Execute(RegisterClientRequest registerClientRequest)
        {
            var response = new RegisterClientResponse();

            if(!validator.IsValid(registerClientRequest, out response))
            {
                return response;
            }

            var client = CreateClient(registerClientRequest);

            clientRepository.Add(client);

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