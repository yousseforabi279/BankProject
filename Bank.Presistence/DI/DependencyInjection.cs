using Bank.Application.contracts;
using Bank.Presistence.Dbcontext;
using Bank.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Presistence.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<Appcontext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Connection")
                ));

            // Register ASP.NET Core Identity
            services.AddIdentityCore<Appuser>()
                    .AddEntityFrameworkStores<Appcontext>();
            services.AddIdentityConfiguration();
            services.AddScoped(typeof(IGeneralRepo<>),
                   typeof(GeneralRepo<>));
            services.AddScoped<IIdentityRepo, identityRepo>();
            services.AddScoped<IJwtTokenService, JwtTokenServices>();
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            return services;
        }

    }
}
