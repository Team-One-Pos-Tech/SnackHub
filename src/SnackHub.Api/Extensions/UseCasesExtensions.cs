using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.UseCases;
using SnackHub.Application.Contracts;
using SnackHub.Application.KitchenOrder.Contracts;
using SnackHub.Application.KitchenOrder.UseCases;
using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.UseCases;
using SnackHub.Application.UseCases;

namespace SnackHub.Extensions;

public static class UseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IRegisterClientUseCase, RegisterClientUseCase>()
            .AddScoped<IGetClientUseCase, GetClientUseCase>()
            .AddScoped<IConfirmOrderUseCase, ConfirmOrderUseCase>()
            .AddScoped<ICancelOrderUseCase, CancelOrderUseCase>()
            .AddScoped<ICheckoutOrderUseCase, CheckoutOrderUseCase>()
            .AddScoped<IListOrderUseCase, ListOrderUseCase>()
            .AddScoped<IGetProductUseCase, GetProductUseCase>()
            .AddScoped<IManageProductUseCase, ManageProductUseCase>()
            .AddScoped<IGetByCategoryUseCase, GetByCategoryUseCase>()
            .AddScoped<ICreateKitchenOrderUseCase, CreateKitchenOrderUseCase>()
            .AddScoped<IUpdateKitchenOrderStatusUseCase, UpdateKitchenOrderStatusUseCase>()
            .AddScoped<IListKitchenOrderUseCase, ListKitchenOrderUseCase>();
    }
}