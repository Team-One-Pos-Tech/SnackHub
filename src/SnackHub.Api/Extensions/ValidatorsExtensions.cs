using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.UseCases;

namespace SnackHub.Extensions;

public static class ValidatorsExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IRegisterClientValidator, RegisterClientValidator>();
    }
}