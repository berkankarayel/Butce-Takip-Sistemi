using BudgetTracking.Application.DTOs.Dashboard;
using BudgetTracking.Application.Interfaces;
using BudgetTracking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracking.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _db;

        public DashboardService(AppDbContext db)
        {
            _db = db;
        }

        // ✅ Genel özet
        public async Task<DashboardSummaryDto> GetSummaryAsync(string userId)
        {
            var totalIncome = await _db.Incomes
                .Where(x => x.UserId == userId)
                .SumAsync(x => (decimal?)x.Amount) ?? 0;

            var totalExpense = await _db.Expenses
                .Where(x => x.UserId == userId)
                .SumAsync(x => (decimal?)x.Amount) ?? 0;

            return new DashboardSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense
            };
        }

        // ✅ Aylık gelir/gider istatistiği
        public async Task<List<MonthlyStatDto>> GetMonthlyAsync(string userId, int year)
        {
            var incomes = await _db.Incomes
                .Where(x => x.UserId == userId && x.Date.Year == year)
                .GroupBy(x => x.Date.Month)
                .Select(g => new { Month = g.Key, Total = g.Sum(x => x.Amount) })
                .ToListAsync();

            var expenses = await _db.Expenses
                .Where(x => x.UserId == userId && x.Date.Year == year)
                .GroupBy(x => x.Date.Month)
                .Select(g => new { Month = g.Key, Total = g.Sum(x => x.Amount) })
                .ToListAsync();

            var result = new List<MonthlyStatDto>();
            for (int month = 1; month <= 12; month++)
            {
                result.Add(new MonthlyStatDto
                {
                    Year = year,
                    Month = month,
                    TotalIncome = incomes.FirstOrDefault(x => x.Month == month)?.Total ?? 0,
                    TotalExpense = expenses.FirstOrDefault(x => x.Month == month)?.Total ?? 0
                });
            }

            return result;
        }

        // ✅ Kategori bazlı istatistik
        public async Task<List<CategoryStatDto>> GetByCategoryAsync(string userId)
        {
            var incomeStats = await _db.Incomes
                .Where(x => x.UserId == userId)
                .GroupBy(x => new { x.CategoryId, x.Category.Name })
                .Select(g => new CategoryStatDto
                {
                    CategoryName = g.Key.Name,
                    TotalAmount = g.Sum(x => x.Amount),
                    IsIncome = true
                })
                .ToListAsync();

            var expenseStats = await _db.Expenses
                .Where(x => x.UserId == userId)
                .GroupBy(x => new { x.CategoryId, x.Category.Name })
                .Select(g => new CategoryStatDto
                {
                    CategoryName = g.Key.Name,
                    TotalAmount = g.Sum(x => x.Amount),
                    IsIncome = false
                })
                .ToListAsync();

            return incomeStats.Concat(expenseStats).ToList();
        }
    }
}
