using Microsoft.AspNetCore.Identity;
using RestaurantApi.Entities;
using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext _context;
        private readonly IPasswordHasher<User> _passwordHaser;

        public AccountService(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHaser)
        {
            _context = dbContext;
            _passwordHaser = passwordHaser;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };

            var hashedPassword = _passwordHaser.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
