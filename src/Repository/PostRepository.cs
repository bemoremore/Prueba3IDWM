using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Prueba3_Backend.src.Data;
using Prueba3_Backend.src.Dtos.PostDtos;
using Prueba3_Backend.src.Interfaces;
using Prueba3_Backend.src.Models;

namespace Prueba3_Backend.src.Repository
{
    public class PostRepository : IPostRepository
    {
        
        private readonly ApplicationDBContext _context;

        private readonly Cloudinary _cloudinary;

        public PostRepository(ApplicationDBContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        public async Task<Post> CreatePost(PostDto postDto, string userEmail)
        {
            try {
                
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == userEmail);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }


                if (postDto.Image == null || postDto.Image.Length == 0)
                {
                    throw new Exception("No se ha subido ninguna imagen");
                }

                // Validar si la imagen es un archivo de imagen válido
                if (postDto.Image.ContentType != "image/jpeg" && postDto.Image.ContentType != "image/png" &&
                    !postDto.Image.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
                    !postDto.Image.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Image must be a JPEG or PNG file");
                }

                
                if (postDto.Image.Length > 5 * 1024 * 1024) 
                {
                    throw new Exception("Image must be less than 5MB");
                }

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(postDto.Image.FileName, postDto.Image.OpenReadStream()),
                    Folder = "posts_images"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                var post = new Post
                {
                    Title = postDto.Title,
                    URL = uploadResult.SecureUrl.ToString(),
                    PublicationDate = DateTime.UtcNow,
                    UserIdPost = user.Id,
                    User = user
                };
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();

                return post;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<PostGetDto>> GetPosts()
        {
            var posts = await _context.Posts
                .Include(p => p.User)
                .Select(p => new PostGetDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    PublicationDate = p.PublicationDate,
                    URL = p.URL,
                    UserEmail = p.User.Email!
                })
                .ToListAsync();

            if (posts == null| posts!.Count == 0)
            {
                throw new Exception("No se encontraron Post's");
            }


            return posts;
        }
    }
}