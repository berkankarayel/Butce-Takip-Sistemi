using BudgetTracking.Application.DTOs.Users;

namespace BudgetTracking.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserListItemDto>> GetAllAsync();
        Task<UserDetailDto?> GetByIdAsync(string id);
        Task<bool> UpdateAsync(string id, UserUpdateDto dto);
        Task<bool> DeleteAsync(string id);
    }
}
