using Leifez.GraphQL.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Leifez.AppStart
{
    public static class QueriesRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<AccountQuery>()//.AddAuthorization()
                    .AddTypeExtension<CollectionQuery>();//.AddAuthorization();
        }
    }
}
