using HotChocolate;
using HotChocolate.AspNetCore;
using Leifez.Accounts;
using Leifez.Application.Domain;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Service.Interfaces;
using Leifez.Application.Service.Services;
using Leifez.Collections;
using Leifez.Common.Configuration;
using Leifez.Common.Web.Auth;
using Leifez.Core.Infrastructure.Mapper;
using Leifez.Core.PostgreSQL;
using Leifez.General.Errors;
using Leifez.Images;
using Leifez.Tags;
using Leifez.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Leifez.General
{
    public static class ServicesConfig
    {
        public static void Register(IServiceCollection services)
        {
            CommonRegistration(services);
            DbRegistration(services);
            AccountRegistration(services);
            CollectionRegistration(services);
            GraphQLRegistration(services);
        }

        private static void CommonRegistration(IServiceCollection services)
        {
            services
                .AddSingleton(MapperConfig.Initialize())

                .AddHttpContextAccessor()
                .AddCors()

                .AddAuthorization(x =>
                {
                    x.AddPolicy("hr-department", builder =>
                        builder
                            .RequireAuthenticatedUser()
                            .RequireRole("hr")
                    );

                    x.AddPolicy("DevDepartment", builder =>
                        builder.RequireRole("dev")
                    );
                })

                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AppConfiguration.Configuration.GetSection("JwtSettings").GetSection("jwt-issuer").Value,
                        ValidateAudience = true,
                        ValidAudience = AppConfiguration.Configuration.GetSection("JwtSettings").GetSection("jwt-audience").Value,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppConfiguration.Configuration.GetSection("JwtSettings").GetSection("jwt-secret").Value)),
                        ValidateIssuerSigningKey = true,
                    };
                });
        }

        private static void DbRegistration(IServiceCollection services)
        {
            var connectionString = AppConfiguration.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            services
                .AddDbContext<IDataContext, DataContext>(options =>
                    options.UseNpgsql(connectionString), ServiceLifetime.Transient);
            //options.UseNpgsql("Server=192.168.31.235;User Id=site_db;Password=JGJS89ydhnflsar312h89HLFDF2;Port=5432;Database=site_db;"));
        }

        private static void AccountRegistration(IServiceCollection services)
        {
            services
                .AddScoped<IAccountDomain, AccountDomain>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<LeifezAuthenticator>();
        }

        private static void CollectionRegistration(IServiceCollection services)
        {
            services
                .AddScoped<ICollectionDomain, CollectionDomain>()
                .AddScoped<ICollectionService, CollectionService>()
                .AddScoped<IImageDomain, ImageDomain>()
                .AddScoped<IImageService, ImageService>()
                .AddScoped<ITagDomain, TagDomain>()
                .AddScoped<ITagService, TagService>();
        }

        private static void GraphQLRegistration(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                    .AddHttpRequestInterceptor(AuthenticationInterceptor())
                    .AddAuthorization()

                    .AddQueryType(t => t.Name("Query"))
                        .AddTypeExtension<CollectionQueries>()
                        .AddTypeExtension<AccountQueries>()
                        .AddTypeExtension<ImageQueries>()
                        .AddTypeExtension<TagQueries>()

                    .AddMutationType(t => t.Name("Mutation"))
                        .AddTypeExtension<AccountMutations>()
                        .AddTypeExtension<ImageMutations>()

                    .AddType<CollectionType>()

                    .AddFiltering()
                    .AddSorting()
                    .EnableRelaySupport()

                    .AddErrorFilter<ErrorFilter>();
        }

        private static HttpRequestInterceptorDelegate AuthenticationInterceptor()
        {
            return (context, executor, builder, cancellationToken) =>
            {
                if (context.GetUser().Identity.IsAuthenticated)
                {
                    builder.SetProperty("currentUser",
                        new CurrentUser(Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                            context.User.Claims.Select(x => $"{x.Type} : {x.Value}").ToList()));
                }

                return new ValueTask(Task.CompletedTask);
            };
        }
    }
}
