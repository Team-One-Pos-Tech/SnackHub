using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Infra.Repositories.InMemory
{
    public class ClientRepository : IClientRepository
    {
        List<Client> clients = new List<Client>
        {
            new Client(new Guid("0ca4d991-b4dd-4e38-984b-c26c67dd6db1"), "John Doe", new CPF("924.142.390-00")),
            new Client(new Guid("21b394fc-ee9a-4dbd-8eef-407139468657"), "Jane Doe", new CPF("078.607.110-95"))
        };
        
        public async Task AddAsync(Client client)
        {
            await Task.Run(() => clients.Add(client));
        }

        public async Task<Client?> GetClientByIdAsync(Guid id)
        {
            // TODO: Scenario for null reference exception
            return await Task.FromResult(clients.FirstOrDefault(client => client.Id == id));
        }

        public async Task<Client?> GetClientByCpfAsync(CPF cpf)
        {
            return await Task.FromResult(clients.FirstOrDefault(client => client.CPF.Equals(cpf)));
        }
    }
}