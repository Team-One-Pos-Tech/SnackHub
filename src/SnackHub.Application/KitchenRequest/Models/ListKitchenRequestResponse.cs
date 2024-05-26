using Flunt.Notifications;

namespace SnackHub.Application.KitchenRequest.Models;

public class ListKitchenRequestResponse : Notifiable<Notification>
{
    public required Guid OrderId { get; init; }
    public required IEnumerable<Item> Items { get; init; } = [];

    public record Item(string ProductName, int Quantity);
}