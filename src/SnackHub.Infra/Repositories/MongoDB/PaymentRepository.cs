using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Infra.Repositories.MongoDB
{
    public sealed class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IMongoDatabase mongoDatabase, string collectionName = "Payments")
            : base(mongoDatabase, collectionName)
        {
        }

        public async Task<Payment?> GetPaymentByIdAsync(string id)
        {
            return await FindByPredicateAsync(p => p.Id.Equals(id));
        }

        public async Task UpdateAsync(Payment payment)
        {
            var filter = Builders<Payment>.Filter.Eq(p => p.Id, payment.Id);
            await MongoCollection.ReplaceOneAsync(filter, payment);
        }
    }
}
