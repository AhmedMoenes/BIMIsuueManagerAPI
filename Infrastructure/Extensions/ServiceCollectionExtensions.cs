using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

            services.TryAddScoped<ICompanyRepository, CompanyRepository>();
            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IProjectRepository, ProjectRepository>();
            services.TryAddScoped<IProjectTeamMemberRepository, ProjectTeamMemberRepository>();
            services.TryAddScoped<IIssueRepository, IssueRepository>();
            services.TryAddScoped<IAreaRepository, AreaRepository>();
            services.TryAddScoped<ICommentRepository, CommentRepository>();
            services.TryAddScoped<IRevitElementRepository, RevitElementRepository>();
            services.TryAddScoped<ILabelRepository, LabelRepository>();
            services.TryAddScoped<IIssueLabelRepository, IssueLabelRepository>();
            services.TryAddScoped<IJwtService, JwtService>();
            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }

    
}
