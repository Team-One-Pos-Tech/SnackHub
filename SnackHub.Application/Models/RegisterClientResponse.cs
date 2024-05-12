namespace SnackHub.Application.Models
{
    public class RegisterClientResponse(bool IsValid)
    {
        public bool IsValid { get; set; } = IsValid;
    };
}