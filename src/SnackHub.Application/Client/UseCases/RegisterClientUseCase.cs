using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Client.UseCases
{
    public class RegisterClientUseCase(IClientRepository clientRepository, IRegisterClientValidator validator) : IRegisterClientUseCase
    {
        public async Task<RegisterClientResponse> Execute(RegisterClientRequest registerClientRequest)
        {
            var response = new RegisterClientResponse();

            var isValid = await validator.IsValid(registerClientRequest, response);

            if(!isValid)
            {
                return response;
            }

            var client = CreateClient(registerClientRequest);

            await clientRepository.AddAsync(client);

            response.Id = client.Id;

            return response;
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