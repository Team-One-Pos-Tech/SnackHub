using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.Models;
using SnackHub.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackHub.Application.Order.UseCases
{
    public class CheckPaymentStatusUseCase : ICheckPaymentStatusUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public CheckPaymentStatusUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CheckPaymentStatusResponse> Execute(CheckPaymentStatusRequest request)
        {
            var response = new CheckPaymentStatusResponse();
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                response.AddNotification("Order", "Order not found.");
                return response;
            }

            response.PaymentStatus = order.Status;
            return response;
        }
    }

}
