using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Owin;
using OWIN.Windsor.DependencyResolverScopeMiddleware;
using System.Web.Http;

namespace OwinApp
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var container = BootstrapContainer();

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            appBuilder.UseWindsorDependencyResolverScope(config, container);
            appBuilder.UseWebApi(config);
        }

        private IWindsorContainer BootstrapContainer()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IDependency>().ImplementedBy<Dependency>().LifestyleScoped());

            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleScoped());

            return container;
        }
    }
}