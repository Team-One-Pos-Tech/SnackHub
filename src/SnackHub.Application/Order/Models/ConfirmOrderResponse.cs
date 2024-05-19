using Flunt.Notifications;

namespace SnackHub.Application.Order.Models;

public class ConfirmOrderResponse : Notifiable<Notification>
{
    public Guid? Id { get; set; }
}