namespace OwinApp
{
    using System.Security.Claims;

    public interface IIdentityDataProvider
    {
        ClaimsIdentity GetIdentity(string id, string secret);
    }
}