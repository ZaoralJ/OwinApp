namespace OwinApp.CustomAuth
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Infrastructure;

    public class APIKeyAuthenticationHandler : AuthenticationHandler<APIKeyAuthenticationOptions>
    {
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var properties = new AuthenticationProperties();
            
            // Find apiKey in default location
            var authorization = Request.Headers.Get("Authorization");
            if (!string.IsNullOrEmpty(authorization))
            {
                if (authorization.StartsWith("Apikey", StringComparison.OrdinalIgnoreCase))
                {
                    var apiKey = authorization.Substring("Apikey".Length).Trim();

                    if (apiKey != Options.Apikey)
                    {
                        return new AuthenticationTicket(null, properties);
                    }
                }
                else
                {
                    return new AuthenticationTicket(null, properties);
                }
            }
            else
            {
                return new AuthenticationTicket(null, properties);
            }

            var userClaim = new Claim(ClaimTypes.Name, "DL");
            var roleClaim = new Claim(ClaimTypes.Role, "apikey");
            var allClaims = new[] { userClaim, roleClaim };

            var identity = new ClaimsIdentity(allClaims, "ApiKey");

            // resulting identity values go back to caller
            return new AuthenticationTicket(identity, properties);
        }
    }
}