using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba3_Backend.src.Dtos.UserDtos;

namespace Prueba3_Backend.src.Interfaces
{
    public interface IUserRepository
    {
        Task<TokenUserDto> LoginAsync(UserDto userDto);

        Task<TokenUserDto> RegisterAsync(UserDto userDto);
    }
}