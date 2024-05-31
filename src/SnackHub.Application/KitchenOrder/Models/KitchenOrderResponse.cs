using Flunt.Notifications;

namespace SnackHub.Application.KitchenOrder.Models;

public class KitchenOrderResponse : Notifiable<Notification>
{
    public required Guid OrderId { get; init; }
    public required IEnumerable<KitchenOrderItem> Items { get; init; } = [];
    public required string Status { get; init; } = string.Empty;

    public record KitchenOrderItem(string ProductName, int Quantity);
}