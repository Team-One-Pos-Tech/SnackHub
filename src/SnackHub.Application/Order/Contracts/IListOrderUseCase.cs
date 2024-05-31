using SnackHub.Application.Order.Models;

namespace SnackHub.Application.Order.Contracts;

public interface IListOrderUseCase
{
    Task<IEnumerable<OrderResponse>> Execute();   
}