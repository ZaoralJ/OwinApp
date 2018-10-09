using System.Threading.Tasks;
using System.Web.Http;

namespace OwinApp
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        public TestController(IDependency dependency)
        {
        }

        [HttpGet]
        [Route("xxx")]
        public int Read()
        {
            return 1;
        }

        [HttpGet]
        [Route("xxx1")]
        public int Read1()
        {
            return 2;
        }

        [HttpGet]
        [Route("xxx1/{wc}/{tag}")]
        public async Task<int> Read2(string wc, string tag)
        {
            return await Task.FromResult(4);
        }

        [HttpPost]
        [Route("xxx1")]
        public int Read3([FromBody] string queryData)
        {
            return 5;
        }
    }
}