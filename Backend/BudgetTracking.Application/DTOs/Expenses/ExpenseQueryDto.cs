namespace BudgetTracking.Application.DTOs.Expenses
{
    public class ExpenseQueryDto
    {
        public int? CategoryId { get; set; }       // Kategori filtresi (opsiyonel)
        public DateTime? From { get; set; }        // Başlangıç tarihi (opsiyonel)
        public DateTime? To { get; set; }          // Bitiş tarihi (opsiyonel)
        public decimal? MinAmount { get; set; }    // Minimum tutar (opsiyonel)
        public decimal? MaxAmount { get; set; }    // Maksimum tutar (opsiyonel)

        public int Page { get; set; } = 1;         // Sayfa numarası (default: 1)
        public int PageSize { get; set; } = 10;    // Sayfa boyutu (default: 10)

        public string? SortBy { get; set; }        // Sıralama kriteri ("date","amount")
        public string? SortDir { get; set; }       // Sıralama yönü ("asc","desc")
    }
}
