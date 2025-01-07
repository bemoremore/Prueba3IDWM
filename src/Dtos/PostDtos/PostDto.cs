using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba3_Backend.src.Dtos.PostDtos
{
    public class PostDto
    {

        [Required]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }
    }
}