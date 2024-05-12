using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Application.UseCases
{
    public class RegisterClientUseCase(IClientRepository clientRepository)
    {
        public RegisterClientResponse Execute(RegisterClientRequest registerClientRequest)
        {
            var client = new Client(registerClientRequest.Name, registerClientRequest.CPF);
            clientRepository.Add(client);

            return new RegisterClientResponse(IsValid: true);
        }
    }
}