using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.Models;
using SnackHub.Domain.Base;
using SnackHub.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackHub.Application.Order.UseCases
{
    public class UpdateOrderStatusUseCase : IUpdateOrderStatusUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderStatusUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UpdateOrderStatusResponse> Execute(UpdateOrderStatusRequest request)
        {
            var response = new UpdateOrderStatusResponse();
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                response.AddNotification("Order", "Order not found.");
                return response;
            }

            try
            {
                order.UpdateOrderStatus(request.Status);
                await _orderRepository.EditAsync(order);               
            }
            catch (DomainException e)
            {
                response.AddNotification("Order", e.Message);
            }
            
            return response;

        }
    }

}
