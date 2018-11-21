using System.Threading.Tasks;

namespace OwinApp
{
    using System.Security.Claims;

    public interface IIdentityDataProvider
    {
        Task<ClaimsIdentity> GetIdentity(string id, string secret);
    }
}