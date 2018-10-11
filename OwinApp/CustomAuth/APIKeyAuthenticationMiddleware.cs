namespace OwinApp.CustomAuth
{
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Infrastructure;

    public class APIKeyAuthenticationMiddleware : AuthenticationMiddleware<APIKeyAuthenticationOptions>
    {
        public APIKeyAuthenticationMiddleware(OwinMiddleware next, APIKeyAuthenticationOptions options)
            : base(next, options)
        {
        }

        protected override AuthenticationHandler<APIKeyAuthenticationOptions> CreateHandler()
        {
            return new APIKeyAuthenticationHandler();
        }
    }
}