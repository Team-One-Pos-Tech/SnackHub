using SnackHub.Domain.Base;

namespace SnackHub.Domain.Entities
{
    public class Client : IAggregateRoot
    {
        public Client(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

    }
}