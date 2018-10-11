namespace OwinApp.BasicAuth
{
    using Owin;

    public static class BasicAuthnMiddlewareExtensions
    {
        public static IAppBuilder UseBasicAuthentication(this IAppBuilder app, BasicAuthenticationMiddleware.CredentialValidationFunction validationFunction)
        {
            var options = new BasicAuthenticationOptions(validationFunction);
            return app.UseBasicAuthentication(options);
        }

        public static IAppBuilder UseBasicAuthentication(this IAppBuilder app, BasicAuthenticationOptions options)
        {
            return app.Use<BasicAuthenticationMiddleware>(options);
        }
    }
}