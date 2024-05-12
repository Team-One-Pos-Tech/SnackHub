using SnackHub.Domain.Base;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Entities
{
    public class Client : IAggregateRoot
    {
        public Client(string name, CPF cPF)
        {
            Name = name;
            CPF = cPF;
        }

        public string Name { get; private set; }

        public CPF CPF { get; private set; }

    }
}