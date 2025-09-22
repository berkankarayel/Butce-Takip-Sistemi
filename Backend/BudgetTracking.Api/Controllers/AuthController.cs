using BudgetTracking.Application.DTOs.Auth;
using BudgetTracking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Yeni kullanıcı kaydı (Sadece Admin erişebilir).
        /// </summary>
        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);

            if (result.StartsWith("Kullanıcı oluşturulamadı"))
                return BadRequest(new { Message = result });

            return Ok(new { Message = result });
        }

        /// <summary>
        /// Kullanıcı giriş yapar ve JWT token döner.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var token = await _authService.LoginAsync(model);
            return Ok(new { Token = token });
        }
    }
}
