using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba3_Backend.src.Dtos.UserDtos
{
    public class TokenUserDto
    {
        public string Mail { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}