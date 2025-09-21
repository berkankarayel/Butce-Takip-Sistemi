using Microsoft.AspNetCore.Identity;


namespace BudgetTracking.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        
        public ICollection<Expense> Expenses { get; set; }
        
        public ICollection<Income> Incomes { get; set; }
    }
}