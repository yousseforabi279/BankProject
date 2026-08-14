using Bank.Application.contracts;
using Bank.Presistence.Dbcontext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Presistence.Repositories
{
    internal class identityRepo : IIdentityRepo
    {
        private readonly UserManager<Appuser> _userManager;

        public identityRepo(UserManager<Appuser> userManager)
        {
           _userManager = userManager;
        }
        public async Task AddToRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new Exception("User not found.");

            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));

                throw new Exception(errors);
            }
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return false;

            return await _userManager.CheckPasswordAsync(
                user,
                password);
        }

        public async Task<bool> CreateUserAsync(string userName, string email, string password)
        {
            var user = new Dbcontext.Appuser
            {
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<string?> GetUserIdAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user?.Id;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user is not null;
        }
    }
}
