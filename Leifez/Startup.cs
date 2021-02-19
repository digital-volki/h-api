using System.Threading.Tasks;
using Leifez.AppStart;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate.Types;
using Unity;
using Leifez.DataAccess.Interfaces;
using Leifez.Application.Service.Interfaces;

namespace Leifez
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LeifezUnityConfig.RegisterComponents();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DataContext>();
            //services.AddSingleton(LeifezUnityConfig.GetConfiguredContainer().Resolve<ICollectionService>());
            QueriesRegistration.Register(services);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseWebSockets();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                //.RequireAuthorization();

                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/graphql");
                    return Task.CompletedTask;
                });

                endpoints.MapControllers();
                //.RequireAuthorization();
            });
        }
    }
}
