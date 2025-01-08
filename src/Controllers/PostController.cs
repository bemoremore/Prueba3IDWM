using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba3_Backend.src.Dtos.PostDtos;
using Prueba3_Backend.src.Interfaces;

namespace Prueba3_Backend.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromForm] PostDto postDto)
        {
            try {
                Console.WriteLine($"Title: {postDto.Title}");
                Console.WriteLine($"Image: {postDto.Image?.FileName}");

                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }
                var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
                Console.WriteLine(email);
                if (email == null)
                {
                    return BadRequest(new { Error = "Email claim not found" });
                }

                var response = await _postRepository.CreatePost(postDto, email);
                return Ok(response);
            } catch (Exception e) {
                
                Console.WriteLine(e.Message);
                return BadRequest(new { Error = e.Message });
            }
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPosts()
        {
            try {
                var response = await _postRepository.GetPosts();
                return Ok(response);
            } catch (Exception e) {
                return BadRequest(new { Error = e.Message });
            }
        }

    }
}