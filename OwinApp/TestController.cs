﻿namespace OwinApp
{
    using System.Web.Http;

    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private readonly IDependency _dependency;

        public TestController(IDependency dependency)
        {
            _dependency = dependency;
        }

        [HttpGet]
        [Route("authorized")]
        ////[Authorize] 
        [Authorize(Roles = "any role, apikey, cz.sy.ad.MSSystem_c")]
        public string Authorized()
        {
            return "Authorized method" + _dependency;
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous()
        {
            return "Anonymous method" + _dependency;
        }
    }
}