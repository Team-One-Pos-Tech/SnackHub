using Flunt.Notifications;

namespace SnackHub.Application.Order.Models;

public class CheckoutOrderResponse : Notifiable<Notification>
{
    public string? TransactionId { get; set; }
    public string? PaymentStatus { get; set; }
    public DateTime? ProcessedAt { get; set; }
};