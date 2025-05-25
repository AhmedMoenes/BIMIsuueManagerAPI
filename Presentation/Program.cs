using Presentation.Initializers;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            builder.Services.AddControllers();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.ConfigureIdentity();
            builder.Services.AddApplication();
            builder.Services.ConfigureJwt(builder.Configuration);
            builder.Services.ConfigureSwaggerWithJwtSupport();
            builder.Services.ConfigureCors();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateIssueDtoValidator>();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddEndpointsApiExplorer();
            #endregion

            #region Application
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                RoleSeeder.SeedRoles(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
