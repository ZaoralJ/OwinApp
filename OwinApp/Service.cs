using Microsoft.Owin.Hosting;
using Polly;
using Polly.Retry;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OwinApp
{
    public class Service
    {
        private IDisposable _webServer;

        public void Start()
        {
            var policy = Policy.Handle<Exception>()
                .RetryAsync(3, (exception, retryCount) =>
                {
                    Console.WriteLine(retryCount);
                });

            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            _webServer = WebApp.Start<Startup>(url: baseAddress);

            var c = 0;

            Parallel.For(0, 100, async i =>
             {
                 c++;

                 await policy.ExecuteAsync(async () =>
                 {

                     using (var client = new HttpClient())
                     {
                         var response = await client.GetAsync(baseAddress + "api/test/xxx1/123456/line.id");
                         Console.WriteLine($"{c} - {i}, {await response.Content.ReadAsStringAsync()}");
                     }
                 });
             });

            Console.WriteLine("Done");
        }

        public void Stop()
        {
            _webServer?.Dispose();
        }
    }
}