using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILabelService, LabelService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IRevitElementService, RevitElementService>();
            services.AddScoped<IProjectTeamMemberService, ProjectTeamMemberService>();
            services.AddScoped<IIssueLabelService, IssueLabelService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
