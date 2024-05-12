using SnackHub.Application.Adapters;
using SnackHub.Application.Models;
using SnackHub.Domain.Entities;

namespace SnackHub.Application.UseCases
{
    public class RegisterUseCase(IClientRepository clientRepository)
    {
        public void Execute(RegisterRequest registerClientRequest)
        {
            var client = new Client(registerClientRequest.Name, registerClientRequest.CPF);
            clientRepository.Add(client);
        }
    }
}