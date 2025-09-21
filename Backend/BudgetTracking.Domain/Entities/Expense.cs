namespace BudgetTracking.Domain.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        // Relations
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
