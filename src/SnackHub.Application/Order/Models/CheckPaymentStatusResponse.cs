using Flunt.Notifications;
using SnackHub.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackHub.Application.Order.Models
{
    public class CheckPaymentStatusResponse : Notifiable<Notification>
    {
        public OrderStatus? PaymentStatus { get; set; }
    }
}
