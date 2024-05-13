namespace SnackHub.Application.Models
{
    public class RegisterClientResponse(bool isValid)
    {
        public Guid? Id { get; set; }

        public bool IsValid { get; set; } = isValid;
    };
}