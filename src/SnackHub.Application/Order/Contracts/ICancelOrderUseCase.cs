using SnackHub.Application.Order.Models;

namespace SnackHub.Application.Order.Contracts;

public interface ICancelOrderUseCase
{
    Task<CancelOrderResponse> Execute(CancelOrderRequest request);
}