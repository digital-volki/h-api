using System.Threading.Tasks;
using System.Web.Http;
using Leifez.AppStart;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Leifez
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            QueriesRegistration.Register(services);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseWebSockets();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            LeifezUnityConfig.RegisterComponents(httpConfiguration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL()
                .RequireAuthorization();

                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/graphql");
                    return Task.CompletedTask;
                });

                endpoints.MapControllers()
                .RequireAuthorization();
            });
        }
    }
}
