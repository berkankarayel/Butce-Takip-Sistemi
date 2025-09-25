using BudgetTracking.Application.DTOs.Incomes;
using BudgetTracking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")] // 🔐 sadece User rolündeki kullanıcı erişebilir
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomesController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        // Kullanıcı Id'sini JWT'den al
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // 📌 1. Yeni gelir ekle
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IncomeCreateDto dto)
        {
            var userId = GetUserId();
            var id = await _incomeService.CreateAsync(userId, dto);
            return Ok(id);
        }

        // 📌 2. Gelir güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IncomeUpdateDto dto)
        {
            var userId = GetUserId();
            await _incomeService.UpdateAsync(userId, id, dto);
            return NoContent();
        }

        // 📌 3. Gelir sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            await _incomeService.DeleteAsync(userId, id);
            return NoContent();
        }

        // 📌 4. Tek gelir getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserId();
            var result = await _incomeService.GetByIdAsync(userId, id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // 📌 5. Listeleme & filtreleme
        [HttpGet("query")]
        public async Task<IActionResult> Query([FromQuery] IncomeQueryDto query)
        {
            var userId = GetUserId();
            var result = await _incomeService.QueryAsync(userId, query);
            return Ok(result);
        }
    }
}
