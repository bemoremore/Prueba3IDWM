using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> CreatePost(PostDto postDto)
        {
            try {
                var userId = User.FindFirst("id")?.Value!; 
                if (string.IsNullOrEmpty(userId)) {
                    return Unauthorized(new { Error = "User Unauthorized" });
                }  
                var response = await _postRepository.CreatePost(postDto, userId);
                return Ok(response);
            } catch (Exception e) {
                return BadRequest(new { Error = e.Message });
            }
        }
        
        [HttpGet]
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