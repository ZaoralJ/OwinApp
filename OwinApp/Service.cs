namespace OwinApp
{
    using System;
    using Microsoft.Owin.Hosting;

    public class Service
    {
        private IDisposable _webServer;

        public void Start()
        {
            var baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            _webServer = WebApp.Start<Startup>(url: baseAddress);
        }

        public void Stop()
        {
            _webServer?.Dispose();
        }
    }
}