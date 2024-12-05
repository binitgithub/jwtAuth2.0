using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using jwtAuth2._0.Data;
using jwtAuth2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace jwtAuth2._0.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JwtAuthDbContext authDbContext;

        public UserRepository(JwtAuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
           return await authDbContext.Users.Include(u => u.UserRoles).ThenInclude(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            authDbContext.Users.Add(user);
            await authDbContext.SaveChangesAsync();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}