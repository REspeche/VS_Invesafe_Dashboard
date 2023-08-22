using BusinessLayer.Classes;
using BusinessLayer.Enum;
using System.Web.Http;

namespace Web.Controllers
{
    [RoutePrefix("api")]
    public class ApiDefaultController : ApiController
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStatus()
        {
            ResponseApi responseApi = new ResponseApi();
            responseApi.result = Api.Result.Ok;

            return Ok(responseApi);
        }
    }
}