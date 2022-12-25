using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenWeather.Web.Data.Entities;
using OpenWeather.Web.Helpers;
using System;
using System.Threading.Tasks;

namespace OpenWeather.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("User");

            var user = await _userHelper.GetUserByEmailAsync("andre2411adm@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "André",
                    LastName = "Fernandes",
                    Email = "andre2411adm@gmail.com",
                    UserName = "andre2411adm@gmail.com",
                    PhoneNumber = "927690241",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }
        }
    }
}