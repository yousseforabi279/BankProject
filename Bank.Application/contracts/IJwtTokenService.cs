using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.contracts
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(
        string userId,
        string email,
        IEnumerable<string> roles);

        Task<string> GenerateRefreshToken(string userid);

    }
}
