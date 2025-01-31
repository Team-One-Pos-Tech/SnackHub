using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using SnackHub.Configuration;

namespace SnackHub.Extensions;

public static class MongoDbExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var settings = configuration.GetSection("Storage:MongoDb").Get<MongoDbSettings>();

        var connection = settings.ConnectionString;

        var mongoClient = new MongoClient(connection);

        serviceCollection.AddSingleton<IMongoClient>(_ => mongoClient);
        serviceCollection.AddSingleton<IMongoDatabase>(_ => mongoClient.GetDatabase(settings.Database));

        return serviceCollection;
    }
    
    // ReSharper disable UnusedMethodReturnValue.Global
    public static IApplicationBuilder UseMongoDbConventions(this IApplicationBuilder app)
    {
        var pack = new ConventionPack
        {
            new EnumRepresentationConvention(BsonType.String)
        };

        ConventionRegistry.Register("EnumStringConvention", pack, _ => true);
        
        return app;
    }
    // ReSharper restore UnusedMethodReturnValue.Global
}