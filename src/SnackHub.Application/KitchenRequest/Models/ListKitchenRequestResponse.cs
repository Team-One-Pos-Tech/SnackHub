using Flunt.Notifications;

namespace SnackHub.Application.KitchenRequest.Models;

public class ListKitchenRequestResponse : Notifiable<Notification>
{
    public required Guid OrderId { get; init; }
    public required IEnumerable<RequestItem> Items { get; init; } = [];
    public required string Status { get; init; } = String.Empty;

    public record RequestItem(string ProductName, int Quantity);
}