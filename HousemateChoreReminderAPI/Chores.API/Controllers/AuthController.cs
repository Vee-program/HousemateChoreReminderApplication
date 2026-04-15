using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.API.DTOs.Auth;
using HousemateChoreReminderAPI.Chores.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace HousemateChoreReminderAPI.Chores.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            try
            {
                var housemate = new Housemate
                {
                    Username = dto.Username,
                    PhoneNumber = dto.PhoneNumber
                };

                var result = await _authService.Register(housemate, dto.Password);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var token = await _authService.Login(dto.Username, dto.Password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot-username")]
        public async Task<IActionResult> ForgotUsername([FromBody] ForgotUsernameDTO dto)
        {
            try
            {
                var username = await _authService.ForgotUsername(dto.Phonenumber);
                return Ok(new { username });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
