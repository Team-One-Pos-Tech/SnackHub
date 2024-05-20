﻿using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Infra.Repositories.InMemory
{
    public class ProductRepositoryMemory : IProductRepository
    {
        private readonly List<Product> products = new List<Product>
        {
            new Product("X-Tudo", Category.Lanche, 22.50m, "Sem Cebola", new List<string> { "imageA1.jpg", "imageA2.jpg" }),
            new Product("Coca-Cola", Category.Bebida, 8.50m, "Sem Gelo", new List<string> { "imageB1.jpg" })
        };

        public async Task AddAsync(Product product)
        {
            await Task.Run(() => products.Add(product));
        }

        public async Task EditAsync(Product product)
        {
            await Task.Run(() =>
            {
                var existingProduct = products.FirstOrDefault(x => x.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Edit(product.Name, product.Category, product.Price, product.Description, product.Images);
                }
            });
        }

        public async Task RemoveAsync(Guid id)
        {
            await Task.Run(() =>
            {
                var product = products.FirstOrDefault(x => x.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                }
            });
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await Task.FromResult(products.FirstOrDefault(x => x.Id == id));
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return product;
        }

        public async Task<IEnumerable<Product>> ListAllAsync()
        {
            return await Task.FromResult(products.AsEnumerable());
        }
    }
}