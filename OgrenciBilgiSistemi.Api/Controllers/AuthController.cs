﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OgrenciBilgiSistemi.Business.Interface;
using OgrenciBilgiSistemi.Entity.Dtos.User.Request;

namespace OgrenciBilgiSistemi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //endpoint - api
        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var result = _authService.RegisterForUser(registerDto);
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var result = _authService.LoginForUser(loginDto);
            return Ok(result);
        }

        [HttpPost("createToken")]
        public IActionResult CreateToken(int userId)
        {
            var result = _authService.CreateToken(userId);
            return Ok(result);
        }
    }
}
