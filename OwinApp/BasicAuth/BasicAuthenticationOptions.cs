namespace OwinApp.BasicAuth
{
    using Microsoft.Owin.Security;

    public class BasicAuthenticationOptions : AuthenticationOptions
    {
        public BasicAuthenticationMiddleware.CredentialValidationFunction CredentialValidationFunction { get; private set; }

        public BasicAuthenticationOptions(BasicAuthenticationMiddleware.CredentialValidationFunction validationFunction)
            : base("Basic")
        {
            CredentialValidationFunction = validationFunction;
        }
    }
}