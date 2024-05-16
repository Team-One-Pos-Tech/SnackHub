using SnackHub.Domain.Base;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Entities
{
    public class Client : IAggregateRoot
    {
        public Client(Guid id, string name, CPF cPF)
        {
            Id = id;
            Name = name;
            CPF = cPF;
        }
        
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public CPF CPF { get; private set; }
    }
}