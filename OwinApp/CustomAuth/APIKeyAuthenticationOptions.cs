namespace OwinApp.CustomAuth
{
    using Microsoft.Owin.Security;

    public class APIKeyAuthenticationOptions : AuthenticationOptions
    {
        public APIKeyAuthenticationOptions(string apikey)
            : base("ApiKey")
        {
            Apikey = apikey;
        }

        public string Apikey { get; }
    }
}