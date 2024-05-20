using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.UseCases
{
    public class GetProductUseCase : IGetProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductResponse?> Execute(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return null;
            
            return new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,
                Images = product.Images
            };
        }

        public async Task<IEnumerable<GetProductResponse>> Execute()
        {
            var products = await _productRepository.ListAllAsync();
            var response = new List<GetProductResponse>();

            foreach (var product in products)
            {
                response.Add(new GetProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category,
                    Price = product.Price,
                    Description = product.Description,
                    Images = product.Images
                });
            }

            return response;
        }
    }
}
