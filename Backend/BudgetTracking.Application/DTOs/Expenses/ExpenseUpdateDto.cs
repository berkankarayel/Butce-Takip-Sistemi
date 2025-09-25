namespace BudgetTracking.Application.DTOs.Expenses
{
    public class ExpenseUpdateDto
    {
        public int CategoryId { get; set; }       // Kategori değişebilir
        public decimal Amount { get; set; }       // Miktar değişebilir
        public string? Description { get; set; }  // Açıklama değişebilir
        public DateTime Date { get; set; }        // Tarih değişebilir
    }
}
