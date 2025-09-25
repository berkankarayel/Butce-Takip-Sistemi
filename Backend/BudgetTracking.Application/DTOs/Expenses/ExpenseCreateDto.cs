namespace BudgetTracking.Application.DTOs.Expenses
{
    public class ExpenseCreateDto
    {
        public int CategoryId { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }=DateTime.UtcNow;
    }
}