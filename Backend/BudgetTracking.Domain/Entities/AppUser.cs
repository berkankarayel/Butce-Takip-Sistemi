using Microsoft.AspNetCore.Identity;

namespace BudgetTracking.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        // Kullanıcı adı-soyadı
        public string FullName { get; set; } = string.Empty;  // ✅ null olmaz

        // Navigasyon property'ler (koleksiyonları boş liste ile başlat)
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Income> Incomes { get; set; } = new List<Income>();
    }
}
