using System.Linq.Expressions;
using MongoDB.Driver;
using SnackHub.Infra.Repositories.Abstractions;

namespace SnackHub.Infra.Repositories.MongoDB;

public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
{
    protected readonly IMongoCollection<TModel> MongoCollection;

    protected BaseRepository(IMongoDatabase mongoDatabase, string collectionName = "")
    {
        if (mongoDatabase is null)
            throw new ArgumentException("MongoDatabase should not be null!");

        MongoCollection = mongoDatabase.GetCollection<TModel>(collectionName == string.Empty ? nameof(TModel) : collectionName);
    }

    public async Task InsertAsync(TModel model)
    {
        await MongoCollection.InsertOneAsync(model);
    }

    public async Task DeleteByPredicateAsync(Expression<Func<TModel, bool>> predicate)
    {
        await MongoCollection.DeleteOneAsync(predicate);
    }

    public async Task<TModel> FindByPredicateAsync(Expression<Func<TModel, bool>> predicate)
    {
        var cursor = await MongoCollection.FindAsync(predicate);
        return await cursor.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TModel>> ListByPredicateAsync(Expression<Func<TModel, bool>> predicate)
    {
        var cursor = await MongoCollection.FindAsync(predicate);
        return await cursor.ToListAsync();
    }

    public async Task<(int pageNumber, int pageSize, int lastPage, IEnumerable<TModel>)> ListPaginateAsync(
        Expression<Func<TModel, bool>> predicate, int pageNumber, int pageSize)
    {
        var count = await MongoCollection.CountDocumentsAsync(predicate);
        var elements = await MongoCollection
            .Find(predicate)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize).ToListAsync();

        var totalPages = (int)count / pageSize;
        if (count % pageSize != 0) totalPages++;

        return (pageNumber, pageSize, totalPages, elements);
    }
}