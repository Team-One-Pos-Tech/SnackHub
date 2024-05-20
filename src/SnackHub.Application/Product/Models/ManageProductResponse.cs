using Flunt.Notifications;

namespace SnackHub.Application.Models
{
    public class ManageProductResponse : Notifiable<Notification>
    {
        public bool IsValid { get; set; }
        public List<string> Notifications { get; set; }
    }
}
