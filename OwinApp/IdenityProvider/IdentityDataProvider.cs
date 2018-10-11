namespace OwinApp
{
    using System.Security.Claims;

    public class IdentityDataProvider : IIdentityDataProvider
    {
        public ClaimsIdentity GetIdentity(string id, string secret)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, id), new Claim(ClaimTypes.Role, "any role") };
            var res = new ClaimsIdentity(claims, "MyAuthenticationType");

            return res;
        }
    }
}