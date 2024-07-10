using Flunt.Notifications;

namespace SnackHub.Application.Order.Models;

public class ConfirmOrderResponse : Notifiable<Notification>
{
    public Guid? OrderId { get; set; }
    public decimal? Total { get; set; }
    public DateTime? CreatedAt { get; set; }
}
