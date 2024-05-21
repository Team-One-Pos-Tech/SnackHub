using SnackHub.Application.Order.Models;

namespace SnackHub.Application.Order.Contracts;

public interface IConfirmOrderUseCase
{
    Task<ConfirmOrderResponse> Execute(ConfirmOrderRequest request);
}