using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.UseCases;
using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.UseCases;

namespace SnackHub.Extensions;

public static class UseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IRegisterClientUseCase, RegisterClientUseCase>()
            .AddScoped<IGetClientUseCase, GetClientUseCase>()
            .AddScoped<IConfirmOrderUseCase, ConfirmOrderUseCase>()
            .AddScoped<ICancelOrderUseCase, CancelOrderUseCase>();
    }
}