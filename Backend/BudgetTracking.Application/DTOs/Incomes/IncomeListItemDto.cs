namespace BudgetTracking.Application.DTOs.Incomes
{
    public class IncomeListItemDto
    {
        public int Id { get; set; }                // Gelirin Id'si
        public int CategoryId { get; set; }        // Kategori Id'si
        public string CategoryName { get; set; } = ""; // Kategori adı (Maaş, Prim...)
        public decimal Amount { get; set; }        // Gelir miktarı
        public string? Description { get; set; }   // Açıklama
        public DateTime Date { get; set; }         // Tarih
    }
}
