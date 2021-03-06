﻿namespace OwinApp
{
    using System.Web.Http;
    using OwinApp.BasicAuth;
    using OwinApp.CustomAuth;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Owin;
    using OWIN.Windsor.DependencyResolverScopeMiddleware;

    public sealed class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void Configuration(IAppBuilder appBuilder)
        {
            var container = BootstrapContainer();

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // NTLM
            ////var listener = (HttpListener)appBuilder.Properties["System.Net.HttpListener"];
            ////listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;

            // api key auth
            appBuilder.UseAPIKeyAuthentication(new APIKeyAuthenticationOptions("ExpectedKey"));

            // set custom identity
            var identityDataProvider = container.Resolve<IIdentityDataProvider>();
            appBuilder.UseBasicAuthentication(async (id, secret) => await identityDataProvider.GetIdentity(id, secret).ConfigureAwait(false));

            appBuilder.UseWindsorDependencyResolverScope(config, container);
            appBuilder.UseWebApi(config);
        }

        private static IWindsorContainer BootstrapContainer()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IDependency>().ImplementedBy<Dependency>().LifestyleScoped());
            container.Register(Component.For<IIdentityDataProvider>().ImplementedBy<IdentityDataProvider>());
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleScoped());

            return container;
        }
    }
}