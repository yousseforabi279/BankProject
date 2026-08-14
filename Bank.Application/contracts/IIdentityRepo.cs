    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.contracts
{
    public interface IIdentityRepo
    {
        Task<bool> CreateUserAsync(
               string userName,
               string email,
               string password);
        Task<bool> UserExistsAsync(string email);
        Task AddToRoleAsync(string userId, string role);
        Task<string?> GetUserIdAsync(string email);
        Task<bool> CheckPasswordAsync(string email,string password);
        Task<IList<string>> GetRolesAsync(string userId);
    }
}
