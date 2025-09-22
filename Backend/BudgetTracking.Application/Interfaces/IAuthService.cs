using BudgetTracking.Application.DTOs.Auth;
using BudgetTracking.Domain.Entities;

namespace BudgetTracking.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto model);
        Task<string> LoginAsync(LoginDto model);
    }
}
