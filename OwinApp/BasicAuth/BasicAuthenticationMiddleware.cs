using System.Threading.Tasks;

namespace OwinApp.BasicAuth
{
    using System.Security.Claims;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Infrastructure;

    public class BasicAuthenticationMiddleware : AuthenticationMiddleware<BasicAuthenticationOptions>
    {
        public delegate Task<ClaimsIdentity> CredentialValidationFunction(string id, string secret);

        public BasicAuthenticationMiddleware(OwinMiddleware next, BasicAuthenticationOptions options)
            : base(next, options)
        {
        }

        protected override AuthenticationHandler<BasicAuthenticationOptions> CreateHandler()
        {
            return new BasicAuthenticationHandler();
        }
    }
}