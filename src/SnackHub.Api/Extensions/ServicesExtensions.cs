using SnackHub.Application.Payment.Contracts;
using SnackHub.Application.Payment.Services;

namespace SnackHub.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IPaymentGatewayService, FakePaymentGatewayService>();
    }
}