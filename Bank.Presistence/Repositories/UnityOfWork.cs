using Bank.Application.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Presistence.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        public IJwtTokenService JwtTokenService => throw new NotImplementedException();

        public IIdentityRepo identityRepo => throw new NotImplementedException();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
