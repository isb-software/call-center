using System.Web.Http;

using DataAccess.Services;

using Entities.QueryOptions;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Call")]
    public class CallController : ApiController
    {
        private readonly CallService callService;

        public CallController()
        {
            this.callService = new CallService();   
        }

        [HttpPost]
        [Route("GetAll")]
        public IHttpActionResult GetAll(TableQueryOptions queryOptions)
        {
            return Ok(callService.GetDatasource(queryOptions));
        }
    }
}
