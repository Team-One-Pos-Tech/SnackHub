using Flunt.Notifications;

namespace SnackHub.Application.Payment.Models
{
    public class ManagePaymentResponse : Notifiable<Notification>
    {
        public new bool IsValid { get; set; }
        public new List<string>? Notifications { get; set; }
    }
}
