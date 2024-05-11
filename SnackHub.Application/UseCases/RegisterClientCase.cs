using SnackHub.Application.Adapters;
using SnackHub.Application.Models;
using SnackHub.Domain.Entities;

namespace SnackHub.Application.UseCases
{
    public class RegisterClientCase(IClientRepository clientRepository)
    {
        public void Execute(RegisterClientRequest registerClientRequest)
        {
            var client = new Client() { Name = registerClientRequest.Name };
            clientRepository.Add(client);
        }
    }
}