using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

namespace SnackHub_Poc.Controllers
{
    public class ClientRepository : IClientRepository
    {
        IEnumerable<Client> clients = new List<Client>
        {
            new Client(new Guid("0ca4d991-b4dd-4e38-984b-c26c67dd6db1"), "John Doe", new CPF("924.142.390-00")),
            new Client(new Guid("21b394fc-ee9a-4dbd-8eef-407139468657"), "Jane Doe", new CPF("078.607.110-95"))
        };


        public ClientRepository() 
        {
        }

        public void Add(Client client)
        {
            throw new NotImplementedException();
        }

        public Client Get(Guid id)
        {
            // TODO: Scenario for null reference exception
            return clients.FirstOrDefault(client => client.Id == id)!;
        }
    }
}