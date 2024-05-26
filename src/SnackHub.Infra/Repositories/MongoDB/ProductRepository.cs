using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Infra.Repositories.MongoDB
{
    public sealed class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDatabase mongoDatabase, string collectionName = "Products")
            : base(mongoDatabase, collectionName)
        {
        }

        public async Task AddAsync(Product product)
        {
            await InsertAsync(product);
        }

        public async Task EditAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            await MongoCollection.ReplaceOneAsync(filter, product);
        }

        public async Task RemoveAsync(Guid id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            await MongoCollection.DeleteOneAsync(filter);
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await FindByPredicateAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<Product>> ListAllAsync()
        {
            return await MongoCollection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await ListByPredicateAsync(p => ids.Contains(p.Id));
        }
        
        public async Task<IEnumerable<Product>> GetByCategory(Category category)
        {
            return await MongoCollection.Find(product => product.Category == category).ToListAsync();
        }
    }

}
