
using Flunt.Notifications;

namespace SnackHub.Application.Authentication.Models
{
    public class SignInResponse(string? IdToken): Notifiable<Notification>
    {
        public string? IdToken { get; init; } = IdToken;
    }
}