using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba3_Backend.src.Dtos.PostDtos
{
    public class PostGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public string URL { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
    }
}