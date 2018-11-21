using Topshelf;

namespace OwinApp
{
    class Program
    {
        static void Main()
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<Service>(s =>
                {
                    s.ConstructUsing(name => new Service());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
            });
        }
    }
}
