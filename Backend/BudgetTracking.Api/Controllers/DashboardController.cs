using BudgetTracking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")] // 🔐 sadece User rolündeki kullanıcı
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        private string GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new UnauthorizedAccessException("UserId bulunamadı");

        // ✅ Genel özet
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var userId = GetUserId();
            var result = await _dashboardService.GetSummaryAsync(userId);
            return Ok(result);
        }

        // ✅ Aylık istatistikler
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthly([FromQuery] int year)
        {
            var userId = GetUserId();
            var result = await _dashboardService.GetMonthlyAsync(userId, year);
            return Ok(result);
        }

        // ✅ Kategori bazlı istatistikler
        [HttpGet("categories")]
        public async Task<IActionResult> GetByCategory()
        {
            var userId = GetUserId();
            var result = await _dashboardService.GetByCategoryAsync(userId);
            return Ok(result);
        }
    }
}
