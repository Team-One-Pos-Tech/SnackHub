namespace SnackHub.Domain.Models.Gateways;

public class SignUpRequest
{
    public SignUpRequest(string name, string cpf, string password, string email)
    {
        Name = name;
        Cpf = cpf;
        Password = password;
        Email = email;
    }

    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}