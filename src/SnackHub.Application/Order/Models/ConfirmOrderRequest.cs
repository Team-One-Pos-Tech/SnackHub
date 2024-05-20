using Flunt.Notifications;

namespace SnackHub.Application.Order.Models;

public class ConfirmOrderRequest : Notifiable<Notification>
{
    public required Guid ClientId { get; init; }
    public required IEnumerable<Item> Items { get; init; } = [];

    public record Item(Guid ProductId, int Quantity);
}