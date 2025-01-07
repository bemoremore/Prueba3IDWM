using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Prueba3_Backend.src.Models
{
    public class User : IdentityUser
    {
        
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}