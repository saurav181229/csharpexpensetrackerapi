using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTrackerAPI.model;
using ExpenseTrackerAPI.services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly TokenService _tokenService;
        public AuthController(IAuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            bool success = await _authService.RegisterUser(userDto.Username, userDto.Password);
            if (!success)
                return BadRequest("User already exists");
            return Ok("User registered successfully");
        }

[HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            var user = await _authService.ValidateUser(dto.Username, dto.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }


    }
     public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}