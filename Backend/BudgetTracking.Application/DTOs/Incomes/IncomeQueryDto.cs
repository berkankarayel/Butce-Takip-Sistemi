namespace BudgetTracking.Application.DTOs.Incomes
{
    public class IncomeQueryDto
    {
        public int? CategoryId { get; set; }       // Kategori filtresi
        public DateTime? From { get; set; }        // Başlangıç tarihi
        public DateTime? To { get; set; }          // Bitiş tarihi
        public decimal? MinAmount { get; set; }    // Min tutar
        public decimal? MaxAmount { get; set; }    // Max tutar

        public int Page { get; set; } = 1;         // Sayfa numarası
        public int PageSize { get; set; } = 10;    // Sayfa boyutu

        public string? SortBy { get; set; }        // "date" / "amount"
        public string? SortDir { get; set; }       // "asc" / "desc"
    }
}
