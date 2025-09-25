using BudgetTracking.Application.DTOs.Common;
using BudgetTracking.Application.DTOs.Incomes;

namespace BudgetTracking.Application.Interfaces
{
    public interface IIncomeService
    {
        Task<int> CreateAsync(string userId, IncomeCreateDto dto);        // Yeni gelir ekle
        Task UpdateAsync(string userId, int id, IncomeUpdateDto dto);     // Gelir güncelle
        Task DeleteAsync(string userId, int id);                          // Gelir sil
        Task<IncomeListItemDto?> GetByIdAsync(string userId, int id);     // Tek gelir getir
        Task<PagedResult<IncomeListItemDto>> QueryAsync(string userId, IncomeQueryDto query); // Listeleme & filtre
    }
}
