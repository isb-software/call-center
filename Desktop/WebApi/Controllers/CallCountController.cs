using System.Web.Http;
using DataAccess.Services;

namespace WebApi.Controllers
{
    [RoutePrefix("api/CallCount")]
    public class CallCountController : ApiController
    {
        private readonly CallCountService callCountService;

        public CallCountController()
        {
            this.callCountService = new CallCountService();
        }

        [Route("Dashboard")]
        [HttpGet]
        public IHttpActionResult GetDashboardCalls()
        {
            return Ok(callCountService.GetDashboardCalls());
        }
    }
}
