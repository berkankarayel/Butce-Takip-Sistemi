using BudgetTracking.Application.DTOs.Expenses;
using BudgetTracking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")] // 🔐 sadece "User" rolüne açık
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // Kullanıcı Id'sini JWT'den al
        private string GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"[DEBUG] JWT'den alınan UserId: {userId}");
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("UserId JWT içinde bulunamadı.");
            return userId;
        }

        // 📌 1. Yeni harcama ekle
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseCreateDto dto)
        {
            var userId = GetUserId();
            Console.WriteLine($"[DEBUG] Expense Create çağrısı - UserId: {userId}, CategoryId: {dto.CategoryId}, Amount: {dto.Amount}");

            var id = await _expenseService.CreateAsync(userId, dto);
            Console.WriteLine($"[DEBUG] Expense başarıyla eklendi - Id: {id}");

            return Ok(id);
        }

        // 📌 2. Harcama güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseUpdateDto dto)
        {
            var userId = GetUserId();
            Console.WriteLine($"[DEBUG] Expense Update - UserId: {userId}, ExpenseId: {id}");

            await _expenseService.UpdateAsync(userId, id, dto);
            return NoContent();
        }

        // 📌 3. Harcama sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            Console.WriteLine($"[DEBUG] Expense Delete - UserId: {userId}, ExpenseId: {id}");

            await _expenseService.DeleteAsync(userId, id);
            return NoContent();
        }

        // 📌 4. Tek harcama getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserId();
            Console.WriteLine($"[DEBUG] Expense GetById - UserId: {userId}, ExpenseId: {id}");

            var result = await _expenseService.GetByIdAsync(userId, id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // 📌 5. Listeleme & filtreleme
        [HttpGet("query")]
        public async Task<IActionResult> Query([FromQuery] ExpenseQueryDto query)
        {
            var userId = GetUserId();
            Console.WriteLine($"[DEBUG] Expense Query - UserId: {userId}, Page: {query.Page}, PageSize: {query.PageSize}");

            var result = await _expenseService.QueryAsync(userId, query);
            return Ok(result);
        }
    }
}
