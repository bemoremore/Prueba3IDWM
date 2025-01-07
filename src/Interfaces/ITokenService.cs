using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba3_Backend.src.Models;

namespace Prueba3_Backend.src.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
    }
}