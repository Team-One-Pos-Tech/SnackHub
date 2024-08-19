using SnackHub.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackHub.Application.Order.Models
{
    public class UpdateOrderStatusRequest
    {
        public required Guid OrderId { get; init; }
        public OrderStatus Status { get; init; }
    }
}
