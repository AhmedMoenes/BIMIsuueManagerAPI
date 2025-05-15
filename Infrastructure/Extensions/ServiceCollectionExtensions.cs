using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CS"),
                    sqlOptions => sqlOptions.MigrationsAssembly("Application")));

            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTeamMemberRepository, ProjectTeamMemberRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IRevitElementRepository, RevitElementRepository>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddScoped<IIssueLabelRepository, IssueLabelRepository>();
            return services;
        }
    }
}
