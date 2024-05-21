using Flunt.Notifications;

namespace SnackHub.Application.Order.Models;

public class CancelOrderRequest : Notifiable<Notification>
{
    public required Guid OrderId { get; init; }
}