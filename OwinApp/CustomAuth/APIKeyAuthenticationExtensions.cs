namespace OwinApp.CustomAuth
{
    using Microsoft.Owin.Extensions;
    using Owin;

    public static class APIKeyAuthenticationExtensions
    {
        public static IAppBuilder UseAPIKeyAuthentication(this IAppBuilder app, APIKeyAuthenticationOptions options)
        {
            app.Use<APIKeyAuthenticationMiddleware>(new APIKeyAuthenticationOptions(options.Apikey));
            app.UseStageMarker(PipelineStage.Authenticate);
            return app;
        }
    }
}