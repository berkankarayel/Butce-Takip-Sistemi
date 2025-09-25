using AutoMapper;
using AutoMapper.QueryableExtensions;
using BudgetTracking.Application.DTOs.Common;
using BudgetTracking.Application.DTOs.Incomes;
using BudgetTracking.Application.Interfaces;
using BudgetTracking.Domain.Entities;
using BudgetTracking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracking.Infrastructure.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public IncomeService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Yeni gelir ekle
        public async Task<int> CreateAsync(string userId, IncomeCreateDto dto)
        {
            var entity = new Income
            {
                UserId = userId,
                CategoryId = dto.CategoryId,
                Amount = dto.Amount,
                Description = dto.Description,
                Date = dto.Date
            };

            _db.Incomes.Add(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }

        // Gelir güncelle
        public async Task UpdateAsync(string userId, int id, IncomeUpdateDto dto)
        {
            var entity = await _db.Incomes
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (entity == null)
                throw new KeyNotFoundException("Gelir bulunamadı.");

            entity.CategoryId = dto.CategoryId;
            entity.Amount = dto.Amount;
            entity.Description = dto.Description;
            entity.Date = dto.Date;

            await _db.SaveChangesAsync();
        }

        // Gelir sil
        public async Task DeleteAsync(string userId, int id)
        {
            var entity = await _db.Incomes
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (entity == null) return;

            _db.Incomes.Remove(entity);
            await _db.SaveChangesAsync();
        }

        // Tek gelir getir
        public async Task<IncomeListItemDto?> GetByIdAsync(string userId, int id)
        {
            return await _db.Incomes
                .Where(x => x.UserId == userId && x.Id == id)
                .Include(x => x.Category)
                .ProjectTo<IncomeListItemDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        // Listeleme & filtreleme
        public async Task<PagedResult<IncomeListItemDto>> QueryAsync(string userId, IncomeQueryDto query)
        {
            var q = _db.Incomes
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
                .ProjectTo<IncomeListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<IncomeListItemDto>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = total,
                Items = items
            };
        }
    }
}
