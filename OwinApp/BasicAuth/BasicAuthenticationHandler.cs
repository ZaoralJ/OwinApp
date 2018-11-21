namespace OwinApp.BasicAuth
{
    using System;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Infrastructure;

    class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var authzValue = Request.Headers.Get("Authorization");
            if (string.IsNullOrEmpty(authzValue) || !authzValue.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var token = authzValue.Substring("Basic ".Length).Trim();
            var claimsIdentity = await TryGetPrincipalFromBasicCredentials(token, Options.CredentialValidationFunction).ConfigureAwait(false);

            if (claimsIdentity == null)
            {
                return null;
            }

            return new AuthenticationTicket(claimsIdentity, new AuthenticationProperties());
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode == 401)
            {
                var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);
                if (challenge != null)
                {
                    Response.Headers.AppendValues("WWW-Authenticate");
                }
            }

            return Task.FromResult<object>(null);
        }

        private static Task<ClaimsIdentity> TryGetPrincipalFromBasicCredentials(string credentials, BasicAuthenticationMiddleware.CredentialValidationFunction validate)
        {
            string pair;
            try
            {
                pair = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
            }
            catch (FormatException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }

            var ix = pair.IndexOf(':');
            if (ix == -1)
            {
                return null;
            }

            var username = pair.Substring(0, ix);
            var pw = pair.Substring(ix + 1);

            return validate(username, pw);
        }
    }
}