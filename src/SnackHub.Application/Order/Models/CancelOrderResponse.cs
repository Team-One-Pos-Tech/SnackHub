using Flunt.Notifications;

namespace SnackHub.Application.Order.Models;

public class CancelOrderResponse : Notifiable<Notification>
{
    public DateTime? CancelledAt { get; set; }
}