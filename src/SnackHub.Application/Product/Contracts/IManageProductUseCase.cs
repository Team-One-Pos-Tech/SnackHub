using SnackHub.Application.Models;

namespace SnackHub.Application.Contracts
{
    public interface IManageProductUseCase
    {
        Task<ManageProductResponse> AddAsync(ManageProductRequest request);
        Task<ManageProductResponse> UpdateAsync(Guid id, ManageProductRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
