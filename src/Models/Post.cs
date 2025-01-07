using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba3_Backend.src.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        // Fecha de publicación (formato a elección que contenga día, mes, año, hora, minuto y segundo).

        public DateTime PublicationDate { get; set; } = DateTime.Now;

        public string URL { get; set; } = string.Empty;

        public string UserIdPost { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}