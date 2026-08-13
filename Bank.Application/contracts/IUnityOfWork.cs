using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.contracts
{
    public interface IUnityOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IJwtTokenService JwtTokenService { get; }
        IIdentityRepo identityRepo { get; }
        

    }
}
