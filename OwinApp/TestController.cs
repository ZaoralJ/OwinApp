namespace OwinApp
{
    using System.Web.Http;

    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        public TestController(IDependency dependency)
        {
        }

        [HttpGet]
        [Route("authorized")]
        ////[Authorize] 
        [Authorize(Roles = "any role, apikey, cz.sy.ad.MSSystem_c")]
        public string Authorized()
        {
            return "Authorized method";
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous()
        {
            return "Anonymous method";
        }
    }
}