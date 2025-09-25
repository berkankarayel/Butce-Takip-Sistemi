namespace BudgetTracking.Application.DTOs.Expenses
{
    public class ExpenseListItemDto
    {
        public int Id { get; set; }               // Harcamanın benzersiz kimliği
        public int CategoryId { get; set; }       // Kategori Id'si
        public string CategoryName { get; set; } = ""; // Kategorinin adı (örn: Yemek)
        public decimal Amount { get; set; }       // Tutar
        public string? Description { get; set; }  // Açıklama
        public DateTime Date { get; set; }        // Tarih
    }
}
