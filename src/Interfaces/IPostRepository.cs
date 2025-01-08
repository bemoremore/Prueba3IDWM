using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Prueba3_Backend.src.Dtos.PostDtos;
using Prueba3_Backend.src.Models;

namespace Prueba3_Backend.src.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> CreatePost(PostDto postDto, string userId);

        Task<List<PostGetDto>> GetPosts();
    }
}