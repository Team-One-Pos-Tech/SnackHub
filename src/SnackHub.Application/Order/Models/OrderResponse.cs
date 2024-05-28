using Flunt.Notifications;

namespace SnackHub.Application.Order.Models;

public class OrderResponse : Notifiable<Notification>
{
    public required Guid Id { get; init; }
    public required IEnumerable<OrderItem> Items { get; init; } = [];
    public required string Status { get; init; } = string.Empty;

    public record OrderItem(string ProductName, int Quantity);
}