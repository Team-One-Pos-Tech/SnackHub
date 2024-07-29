using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackHub.Application.Order.Models
{
    public class CheckPaymentStatusRequest
    {
        public required Guid OrderId { get; init; }
    }
}
