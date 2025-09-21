namespace BudgetTracking.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; } // true = gelir, false = gider

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Income> Incomes { get; set; }
    }
}
