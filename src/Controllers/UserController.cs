using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prueba3_Backend.src.Dtos.UserDtos;
using Prueba3_Backend.src.Interfaces;

namespace Prueba3_Backend.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserDto userDto)
        {
            try {
                var response = await _userRepository.LoginAsync(userDto);
                return Ok(response);
            } catch (AuthenticationException e) {
                return StatusCode(401, new { Error = e.Message });
            } catch (Exception e) {
                return StatusCode(500, new { Error = e.Message });
            }
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserDto userDto)
        {
            try {
                var response = await _userRepository.RegisterAsync(userDto);
                return Ok(response);
            } catch (Exception e) {
                return BadRequest(new { Error = e.Message });
            }
        }

    }
}