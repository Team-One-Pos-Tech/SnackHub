using SnackHub.Domain.Contracts;
using SnackHub.Infra.Repositories.MongoDB;

namespace SnackHub.Extensions;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IClientRepository, ClientRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IKitchenRequestRepository, KitchenRequestRepository>();
    }
}