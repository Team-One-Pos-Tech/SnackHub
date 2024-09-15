namespace SnackHub.Domain.Models.Gateways;

public record SignInRequest
{

    public SignInRequest(string cpf, string password)
    {
        Cpf = cpf;
        Password = password;
    }

    public string Cpf { get; set; }

    public string Password { get; set; }
}