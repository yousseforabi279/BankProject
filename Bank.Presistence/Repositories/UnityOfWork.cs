using Bank.Application.contracts;
using Bank.Presistence.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Presistence.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly Appcontext _context;
        public IJwtTokenService JwtTokenService { get; }
        public IIdentityRepo identityRepo { get; }
        public UnityOfWork(
            Appcontext context,
            IJwtTokenService jwtTokenService,
            IIdentityRepo identityRepo)
        {
            _context = context;
            JwtTokenService = jwtTokenService;
            this.identityRepo = identityRepo;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
