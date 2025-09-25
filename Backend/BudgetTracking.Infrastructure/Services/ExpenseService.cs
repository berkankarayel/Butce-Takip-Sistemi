using AutoMapper;
using AutoMapper.QueryableExtensions;
using BudgetTracking.Application.DTOs.Common;
using BudgetTracking.Application.DTOs.Expenses;
using BudgetTracking.Application.Interfaces;
using BudgetTracking.Domain.Entities;
using BudgetTracking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracking.Infrastructure.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ExpenseService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Yeni harcama ekle
        public async Task<int> CreateAsync(string userId, ExpenseCreateDto dto)
        {
            var entity = new Expense
            {
                UserId = userId,
                CategoryId = dto.CategoryId,
                Amount = dto.Amount,
                Description = dto.Description,
                Date = dto.Date
            };

            _db.Expenses.Add(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }

        // Harcama güncelle
        public async Task UpdateAsync(string userId, int id, ExpenseUpdateDto dto)
        {
            var entity = await _db.Expenses
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (entity == null)
                throw new KeyNotFoundException("Harcama bulunamadı.");

            entity.CategoryId = dto.CategoryId;
            entity.Amount = dto.Amount;
            entity.Description = dto.Description;
            entity.Date = dto.Date;

            await _db.SaveChangesAsync();
        }

        // Harcama sil
        public async Task DeleteAsync(string userId, int id)
        {
            var entity = await _db.Expenses
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (entity == null) return;

            _db.Expenses.Remove(entity);
            await _db.SaveChangesAsync();
        }

        // Tek harcama getir
        public async Task<ExpenseListItemDto?> GetByIdAsync(string userId, int id)
        {
            return await _db.Expenses
                .Where(x => x.UserId == userId && x.Id == id)
                .Include(x => x.Category)
                .ProjectTo<ExpenseListItemDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        // Listeleme & filtreleme
        public async Task<PagedResult<ExpenseListItemDto>> QueryAsync(string userId, ExpenseQueryDto query)
        {
            var q = _db.Expenses
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Include(x => x.Category)
                .AsQueryable();

            if (query.CategoryId.HasValue)
                q = q.Where(x => x.CategoryId == query.CategoryId.Value);
            if (query.From.HasValue)
                q = q.Where(x => x.Date >= query.From.Value);
            if (query.To.HasValue)
                q = q.Where(x => x.Date <= query.To.Value);
            if (query.MinAmount.HasValue)
                q = q.Where(x => x.Amount >= query.MinAmount.Value);
            if (query.MaxAmount.HasValue)
                q = q.Where(x => x.Amount <= query.MaxAmount.Value);

            // sıralama
            var sortBy = (query.SortBy ?? "date").ToLower();
            var sortDir = (query.SortDir ?? "desc").ToLower();
            q = (sortBy, sortDir) switch
            {
                ("amount", "asc") => q.OrderBy(x => x.Amount),
                ("amount", "desc") => q.OrderByDescending(x => x.Amount),
                ("date", "asc") => q.OrderBy(x => x.Date),
                _ => q.OrderByDescending(x => x.Date)
            };

            var total = await q.CountAsync();

            var items = await q
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ProjectTo<ExpenseListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<ExpenseListItemDto>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = total,
                Items = items
            };
        }
    }
}
