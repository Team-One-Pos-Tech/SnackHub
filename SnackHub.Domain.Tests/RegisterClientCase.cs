
using SnackHub.Application.Tests;

namespace SnackHub.Domain.Tests
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