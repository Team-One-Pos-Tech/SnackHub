using SnackHub.Application.KitchenOrder.Contracts;
using SnackHub.Application.KitchenOrder.Models;
using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.Models;
using SnackHub.Application.Payment.Contracts;
using SnackHub.Application.Payment.Models;
using SnackHub.Domain.Base;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Order.UseCases;

public class CheckoutOrderUseCase : ICheckoutOrderUseCase 
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentGatewayService _paymentGatewayService;

    private readonly ICreateKitchenOrderUseCase _kitchenOrderUseCase;
    
    public CheckoutOrderUseCase(
        IOrderRepository orderRepository, 
        IPaymentGatewayService paymentGatewayService,
        ICreateKitchenOrderUseCase kitchenOrderUseCase)
    {
        _orderRepository = orderRepository;
        _paymentGatewayService = paymentGatewayService;
        _kitchenOrderUseCase = kitchenOrderUseCase;
    }
    
    public async Task<CheckoutOrderResponse> Execute(CheckoutOrderRequest request)
    {
        var response = new CheckoutOrderResponse();
        
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order is null)
        {
            response.AddNotification(nameof(request.OrderId), "Order not found");
            return response;
        }
        
        if (order.Status != OrderStatus.Confirmed)
        {
            response.AddNotification(nameof(request.OrderId), "Only confirmed orders can be checked out");
            return response;
        }

        try
        {
            var paymentRequest = new PaymentRequest(order.Total, request.PaymentMethod, new { request.OrderId });
            var paymentResponse = await _paymentGatewayService.Process(paymentRequest);
            var isSuccessful = PaymentStatus.Success.Equals(paymentResponse.Status);
            
            order.Checkout(isSuccessful);
            
            await _orderRepository.EditAsync(order);

            response.TransactionId = paymentResponse.TransactionId;
            response.PaymentStatus = Enum.GetName(paymentResponse.Status);
            response.ProcessedAt = order.UpdatedAt;
           
            // TODO: raise event that will trigger "KitchenOrder" creation
            // TODO: move this function to a event like in a near future(MediatR ??)
            await SubmitKitchenOrderAsync(order);
        }
        catch (DomainException e)
        {
            response.AddNotification(nameof(request.OrderId), e.Message);
        }
        
        return response;
    }

    private async Task SubmitKitchenOrderAsync(Domain.Entities.Order order)
    {
        await _kitchenOrderUseCase.Execute(new CreateKitchenOrderRequest
        {
            OrderId = order.Id
        });
    }
}