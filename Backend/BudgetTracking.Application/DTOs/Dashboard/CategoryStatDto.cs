namespace BudgetTracking.Application.DTOs.Dashboard
{
    public class CategoryStatDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public bool IsIncome { get; set; }  // Gelir mi gider mi
    }
}
