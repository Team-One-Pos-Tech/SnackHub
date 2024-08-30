namespace SnackHub.Domain.Models;

public class SignUpRequest
{
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Password { get; set; }
}