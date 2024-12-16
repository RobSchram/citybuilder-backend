using BCrypt.Net;
using Logic.Interfaces;
using Logic.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            User Olduser = await _userRepository.GetUserByUsernameAsync(userDto.username);
            var user = new User
            {
                UserName = userDto.username,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.password)
            };
            if (Olduser == null)
            {
                await _userRepository.AddUserAsync(user);
            }
            else 
            {
                throw new ArgumentException("Username is already taken.");
            }
            
        }

        public async Task<User> AuthenticateAsync(UserDto userDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userDto.username);

            if (user == null || !VerifyPassword(userDto.password, user.Password))
            {
                return null;
            }

            return user;
        }
        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedPasswordHash);
        }

    }
}
