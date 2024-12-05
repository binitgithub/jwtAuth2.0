using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jwtAuth2._0.DTOs;
using jwtAuth2._0.Models;
using jwtAuth2._0.Repositories;
using jwtAuth2._0.Services;
using Microsoft.AspNetCore.Mvc;

namespace jwtAuth2._0.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JwtAuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly JwtService jwtService;
        private readonly IMapper mapper;

        public JwtAuthController(IUserRepository userRepository, JwtService jwtService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = mapper.Map<User>(registerUserDto);
            await userRepository.RegisterUserAsync(user, registerUserDto.Password);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (!await userRepository.ValidateUserAsync(loginUserDto.Username, loginUserDto.Password))
            return Unauthorized();

            var user = await userRepository.GetUserByUsernameAsync(loginUserDto.Username);
            var token = jwtService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }
}