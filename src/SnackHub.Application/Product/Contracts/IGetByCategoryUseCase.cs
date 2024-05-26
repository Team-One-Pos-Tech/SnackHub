using SnackHub.Application.Models;
using SnackHub.Domain.Entities;

namespace SnackHub.Application.Contracts;

public interface IGetByCategoryUseCase
{
    Task<IEnumerable<GetProductResponse>> Get(Category category);
}