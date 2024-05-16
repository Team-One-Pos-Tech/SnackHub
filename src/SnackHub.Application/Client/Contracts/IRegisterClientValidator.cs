using SnackHub.Application.Client.Models;

namespace SnackHub.Application.Client.Contracts
{
    public interface IRegisterClientValidator
    {
        bool IsValid(RegisterClientRequest registerClientRequest, out RegisterClientResponse response);
    }
}