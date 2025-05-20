using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Application.Interfaces;
using Application.Services;
namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CS"),
                    sqlOptions => sqlOptions.MigrationsAssembly("Infrastructure")));

            services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTeamMemberRepository, ProjectTeamMemberRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IRevitElementRepository, RevitElementRepository>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddScoped<IIssueLabelRepository, IssueLabelRepository>();
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }

    }

    
}
