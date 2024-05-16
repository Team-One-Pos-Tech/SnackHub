using Flunt.Notifications;

namespace SnackHub.Application.Client.Models
{
    public class RegisterClientResponse() : Notifiable<Notification>
    {
        public Guid? Id { get; set; }

    };
}