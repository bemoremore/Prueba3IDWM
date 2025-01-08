using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Prueba3_Backend.src.Dtos.UserDtos;
using Prueba3_Backend.src.Interfaces;
using Prueba3_Backend.src.Models;

namespace Prueba3_Backend.src.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        
        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<TokenUserDto> LoginAsync(UserDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Mail);

            if (user == null)
            {
                throw new AuthenticationException("Usuario no encontrado");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

            if (!result.Succeeded)
            {
                throw new AuthenticationException("Contraseña incorrecta");
            }

            return new TokenUserDto
            {
                Token = await _tokenService.CreateTokenAsync(user),
                Mail = user.Email!
            };
        }

        public async Task<TokenUserDto> RegisterAsync(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Mail) || string.IsNullOrEmpty(userDto.Password))
            {
                throw new Exception("La contraseña y el correo son requeridos");
            }

            if (!await VerificarMail(userDto.Mail))
            {
                throw new Exception("El correo ya esta registrado");
            }

            var user = new User
            {
                Email = userDto.Mail,
                UserName = userDto.Mail.Split('@')[0]
            };

            var createUser = await _userManager.CreateAsync(user, userDto.Password);

            if (!createUser.Succeeded)
            {
                Console.WriteLine("Soy tu usuario: " + createUser);
                Console.WriteLine("Soy tu error: " + createUser.Errors);
                throw new Exception("Error al crear el usuario");
            }

            return new TokenUserDto
            {
                Token = await _tokenService.CreateTokenAsync(user),
                Mail = user.Email
            };


        }
        public async Task<bool> VerificarMail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null; 
        }
    }

    
}