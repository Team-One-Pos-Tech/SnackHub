namespace SnackHub.Domain.Entities
{
    public class Client(string name, string cPF)
    {
        public string Name { get; set; } = name;
        public string CPF { get; set; } = cPF;
    }
}
