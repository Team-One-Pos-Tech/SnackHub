using SnackHub.Application.Client.Models;

namespace SnackHub.Application.Client.Contracts
{
    public interface IRegisterClientValidator
    {
        Task<bool> IsValid(RegisterClientRequest registerClientRequest, RegisterClientResponse response);
    }
}