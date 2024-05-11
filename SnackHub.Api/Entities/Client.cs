using SnackHub.Domain.Base;

namespace SnackHub.Domain.Entities
{
    public class Client : IAggregateRoot
    {
        public Client(string name, string cPF)
        {
            Name = name;
            CPF = cPF;
        }

        public string Name { get; private set; }

        public string CPF { get; private set; }

    }
}