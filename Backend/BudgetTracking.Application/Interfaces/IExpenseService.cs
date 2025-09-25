using BudgetTracking.Application.DTOs.Common;
using BudgetTracking.Application.DTOs.Expenses;

namespace BudgetTracking.Application.Interfaces
{
    public interface IExpenseService
    {
        Task<int> CreateAsync(string userId, ExpenseCreateDto dto);

        Task UpdateAsync(string userId, int id, ExpenseUpdateDto dto);

        Task DeleteAsync(string userId, int id);

        Task<ExpenseListItemDto> GetByIdAsync(string userId, int id);
        
        Task<PagedResult<ExpenseListItemDto>> QueryAsync(string userId, ExpenseQueryDto query);
    }
}