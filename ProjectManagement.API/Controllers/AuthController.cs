using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL.Data;
using ProjectManagement.Entities.Models;
using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.BLL.Services;
using ProjectManagement.BLL.Interfaces;
using BCrypt.Net;

namespace ProjectManagement.API.Controllers
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

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result == null)
                return BadRequest("Username already exists");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null)
                return Unauthorized("Invalid username or password");

            return Ok(result);
        }
    }
}
