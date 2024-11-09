using SnackHub.Application.Payment.Contracts;
using SnackHub.Application.Payment.Services;
using SnackHub.Domain.Contracts;
using SnackHub.Services;

namespace SnackHub.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPaymentGatewayService, FakePaymentGatewayService>();
        serviceCollection.AddScoped<IAuthService, JwtAuthService>();

        return serviceCollection;
    }
}