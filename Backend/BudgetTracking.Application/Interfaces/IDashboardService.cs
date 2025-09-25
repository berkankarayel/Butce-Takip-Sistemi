using BudgetTracking.Application.DTOs.Dashboard;

namespace BudgetTracking.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync(string userId);
        Task<List<MonthlyStatDto>> GetMonthlyAsync(string userId, int year);
        Task<List<CategoryStatDto>> GetByCategoryAsync(string userId);
    }
}
