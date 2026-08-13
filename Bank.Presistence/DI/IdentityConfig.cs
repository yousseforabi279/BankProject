using Bank.Presistence.Dbcontext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Presistence.DI
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(
        this IServiceCollection services)
        {
            services
                .AddIdentityCore<Appuser>(options =>
                {
                    // Password settings
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;

                    // User settings
                    options.User.RequireUniqueEmail = true;

                    // Lockout settings
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan =
                        TimeSpan.FromMinutes(5);
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Appcontext>();
            return services;
        }
    }
}
