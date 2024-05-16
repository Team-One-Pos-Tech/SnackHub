using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.UseCases;

namespace SnackHub.Extensions;

public static class UseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IRegisterClientUseCase, RegisterClientUseCase>()
            .AddScoped<IGetClientUseCase, GetClientUseCase>();
    }
}