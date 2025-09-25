namespace BudgetTracking.Application.DTOs.Dashboard
{
    public class MonthlyStatDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
    }
}
