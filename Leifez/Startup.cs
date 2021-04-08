using System.Threading.Tasks;
using Leifez.Common.Configuration;
using Leifez.General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Leifez
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment Environment { get; set; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
            Configuration = new ConfigurationBuilder()
                     .AddJsonFile($"AppSettings.{Environment.EnvironmentName}.json")
                     .Build();
            new AppConfiguration(Configuration, Environment.EnvironmentName);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ServicesConfig.Register(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            
            app.UseWebSockets();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();

                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/graphql");
                    return Task.CompletedTask;
                });
            });
        }
    }
}
