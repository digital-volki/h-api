using Leifez.Common.Web.Common.ActionResults;
using Leifez.Core.BearerAuth;
using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Leifez.Common.Web.BearerAuth.Filters
{
    public class LeifezAuthenticateAttribute : Attribute, IAuthenticationFilter
    {
        private static string Realm => "Leifez";

        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                return;
            }

            if (authorization.Scheme.ToLower() != "bearer")
            {
                return;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                return;
            }

            context.Principal = await Task.Run(() => LeifezAuthenticator.Authenticate(authorization.Parameter), cancellationToken);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var parameter = string.IsNullOrWhiteSpace(Realm) ? null : "realm=\"" + Realm + "\"";
            var challenge = new AuthenticationHeaderValue("Bearer", parameter);
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.FromResult(0);
        }
    }
}
