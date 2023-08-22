using BusinessLayer.Classes;
using BusinessLayer.DTO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class InformationController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetReports()
        {
            ResponseReports response = new ResponseReports();
            InformationDto informationDto = new InformationDto();
            await Task.Factory.StartNew(() =>
            {
                response = informationDto.GetReports();
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        
    }

}