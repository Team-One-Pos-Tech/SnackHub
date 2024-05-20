using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Application.UseCases
{
    public class ManageProductUseCase : IManageProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public ManageProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ManageProductResponse> AddAsync(ManageProductRequest request)
        {
            var product = new Product(request.Name, request.Category, request.Price, request.Description, request.Images);
            await _productRepository.AddAsync(product);

            return new ManageProductResponse { IsValid = true };
        }

        public async Task<ManageProductResponse> UpdateAsync(Guid id, ManageProductRequest request)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return new ManageProductResponse { IsValid = false, Notifications = new List<string> { "Product not found." } };

            product.Edit(request.Name, request.Category, request.Price, request.Description, request.Images);
            await _productRepository.EditAsync(product);

            return new ManageProductResponse { IsValid = true };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return false;

            await _productRepository.RemoveAsync(id);
            return true;
        }
    }
}
