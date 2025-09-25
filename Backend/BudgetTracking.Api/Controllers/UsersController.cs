using BudgetTracking.Application.DTOs.Users;
using BudgetTracking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // 🔐 sadece Admin erişebilir
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // ✅ Tüm kullanıcıları listele
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // ✅ Tek kullanıcı getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // ✅ Güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UserUpdateDto dto)
        {
            var result = await _userService.UpdateAsync(id, dto);
            if (!result) return NotFound("Kullanıcı bulunamadı.");
            return Ok("Kullanıcı güncellendi.");
        }

        // ✅ Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result) return NotFound("Kullanıcı bulunamadı.");
            return Ok("Kullanıcı silindi.");
        }
    }
}
