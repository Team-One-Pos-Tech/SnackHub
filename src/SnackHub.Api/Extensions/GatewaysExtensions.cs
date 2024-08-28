using SnackHub.Domain.Contracts;
using SnackHub.Infra.Gateways;
using SnackHub.Infra.Repositories.MongoDB;

namespace SnackHub.Extensions;

public static class GatewaysExtensions
{
    public static IServiceCollection AddGateways(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ISignUpFunctionGateway, SignUpFunctionGateway>();
    }
}