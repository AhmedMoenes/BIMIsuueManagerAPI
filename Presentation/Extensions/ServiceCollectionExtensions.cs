using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy("CORS_Policy", policy =>
                    policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
                ));
        }
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
            return services;
        }

    }
}
