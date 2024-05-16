using Flunt.Notifications;

namespace SnackHub.Application.Models
{
    public class RegisterClientResponse() : Notifiable<Notification>
    {
        public Guid? Id { get; set; }

    };
}