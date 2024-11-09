using SnackHub.Configuration;
using SnackHub.Domain.Contracts;
using SnackHub.Infra.Gateways;

namespace SnackHub.Extensions;

public static class GatewaysExtensions
{
    public static IServiceCollection AddGateways(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection;
    }
}