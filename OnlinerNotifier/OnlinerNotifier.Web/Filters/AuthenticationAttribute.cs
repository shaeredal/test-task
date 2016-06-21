using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace OnlinerNotifier.Filters
{
    public class AuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {

            var userIdCookie = context.Request.Headers.GetCookies("User").FirstOrDefault();
            if (userIdCookie != null)
            {
                int userId;
                if (int.TryParse(userIdCookie["User"].Value, out userId))
                {
                    context.Principal = new Principal(userId);
                }
                else
                {
                    context.ErrorResult = new AuthenticationFailureResult("Incorrect cookie.", context.Request);
                }
            }
            else
            {
                context.ErrorResult = new AuthenticationFailureResult("Cookie is missing.", context.Request);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public void Challenge(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
        }
    }
}