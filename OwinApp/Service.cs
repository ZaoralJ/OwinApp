using Microsoft.Owin.Hosting;
using System;

namespace OwinApp
{
    public class Service
    {
        private IDisposable _webServer;

        public void Start()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            _webServer = WebApp.Start<Startup>(url: baseAddress);
        }

        public void Stop()
        {
            _webServer?.Dispose();
        }
    }
}