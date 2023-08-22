using BusinessLayer.Classes;
using BusinessLayer.DTO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Filters;
using Web.Helpers;

namespace Web.Controllers
{
    public class CommonController : BaseController
    {

        /// <summary>
        /// Gets the list country.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> GetListCountry()
        {
            ResponseItemCombo response = new ResponseItemCombo();
            CommonDto commonDto = new CommonDto();
            await Task.Factory.StartNew(() =>
            {
                response = commonDto.GetListCountry();
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> GetListyesNo()
        {
            ResponseItemCombo response = new ResponseItemCombo();
            CommonDto commonDto = new CommonDto();
            await Task.Factory.StartNew(() =>
            {
                response = commonDto.GetListYesNo();
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> GetListTypeAccounts()
        {
            ResponseItemCombo response = new ResponseItemCombo();
            CommonDto commonDto = new CommonDto();
            await Task.Factory.StartNew(() =>
            {
                response = commonDto.GetListTypeAccounts();
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> GetListTypeOperations()
        {
            ResponseItemCombo response = new ResponseItemCombo();
            CommonDto commonDto = new CommonDto();
            await Task.Factory.StartNew(() =>
            {
                response = commonDto.GetListTypeOperations();
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> GetListTypeProjects()
        {
            ResponseItemCombo response = new ResponseItemCombo();
            CommonDto commonDto = new CommonDto();
            await Task.Factory.StartNew(() =>
            {
                response = commonDto.GetListTypeProjects();
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        [CacheFilter(Duration = 5, Order = 2)]
        public async Task<JsonResult> VerifyCaptcha(string token)
        {
            ResponseCaptcha response = new ResponseCaptcha();
            response.success = false;
            await Task.Factory.StartNew(() =>
            {
                response.success = RecaptchaHelper.Validate(token);
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}