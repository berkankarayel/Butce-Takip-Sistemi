namespace BudgetTracking.Application.DTOs.Incomes
{
    public class IncomeCreateDto
    {
        public int CategoryId { get; set; }        // Gelirin kategorisi (Maaş, Prim, vb.)
        public decimal Amount { get; set; }        // Gelir miktarı
        public string? Description { get; set; }   // Açıklama
        public DateTime Date { get; set; } = DateTime.UtcNow; // Tarih
    }
}
