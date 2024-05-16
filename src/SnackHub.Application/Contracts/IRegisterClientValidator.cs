using SnackHub.Application.Models;

namespace SnackHub.Application.Contracts
{
    public interface IRegisterClientValidator
    {
        bool IsValid(RegisterClientRequest registerClientRequest, out RegisterClientResponse response);
    }
}