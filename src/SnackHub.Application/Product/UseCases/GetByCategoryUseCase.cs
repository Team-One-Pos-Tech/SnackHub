using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Application.UseCases;

public class GetByCategoryUseCase(IProductRepository productRepository) : IGetByCategoryUseCase
{
    public async Task<IEnumerable<GetProductResponse>> Get(Category category)
    {
        var products = await productRepository.GetByCategory(category);
        
        return products.Select(product => new GetProductResponse(
            product.Id,
            product.Name,
            product.Category,
            product.Price,
            product.Description,
            product.Images
        ));
    }
}