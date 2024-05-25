using SnackHub.Application.Order.Models;

namespace SnackHub.Application.Order.Contracts;

public interface ICheckoutOrderUseCase
{
    Task<CheckoutOrderResponse> Execute(CheckoutOrderRequest request);
}