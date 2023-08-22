using BusinessLayer.Classes;
using BusinessLayer.DTO;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Filters;
using static BusinessLayer.Enum.Tables;

namespace Web.Controllers
{
    public class InvestmentController : BaseController
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetMyInvestments
            (
            String search,
            String type
            )
        {
            ResponseMyInvestment response = new ResponseMyInvestment();
            InvestmentDto investmentDto = new InvestmentDto();
            await Task.Factory.StartNew(() =>
            {
                response = investmentDto.GetMyInvestments(
                    Convert.ToInt64(Session["id"]),
                    search,
                    (type != null && !type.Equals("")) ? Convert.ToInt16(type) : 0
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pxcId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetMyInvest
        (
            String pxcId
        )
        {
            ResponseMyInvest response = new ResponseMyInvest();
            InvestmentDto investmentDto = new InvestmentDto();
            await Task.Factory.StartNew(() =>
            {
                response = investmentDto.GetMyInvest(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt64(pxcId)
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pxcId"></param>
        /// <param name="amount"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SaveInvestToSale
        (
            String pxcId,
            String amount,
            String fraction,
            String agree
        )
        {
            ResponseMessage response = new ResponseMessage();
            InvestmentDto investmentDto = new InvestmentDto();
            await Task.Factory.StartNew(() =>
            {
                response = investmentDto.SaveSale(
                    Convert.ToInt64(pxcId),
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt64(amount),
                    Math.Round(Convert.ToDouble(fraction), 2),
                    (yesNo)System.Enum.ToObject(typeof(yesNo), Convert.ToInt16(agree))
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pxcId"></param>
        /// <param name="amount"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> RemoveInvestToSale
        (
            String pxcId,
            String sinId
        )
        {
            ResponseMessage response = new ResponseMessage();
            InvestmentDto investmentDto = new InvestmentDto();
            await Task.Factory.StartNew(() =>
            {
                response = investmentDto.RemoveSale(
                    Convert.ToInt64(pxcId),
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt64(sinId)
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetTypeInverter()
        {
            ResponseTypeInverter response = new ResponseTypeInverter();
            InvestmentDto investmentDto = new InvestmentDto();
            await Task.Factory.StartNew(() =>
            {
                response = investmentDto.GetTypeInverter(
                    Convert.ToInt64(Session["id"])
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeInvest"></param>
        /// <param name="question1"></param>
        /// <param name="question2"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SaveTypeInverter (
            string typeInvest,
            string question1,
            string question2,
            string agree
        )
        {
            ResponseMessage response = new ResponseMessage();
            InvestmentDto investmentDto = new InvestmentDto();
            await Task.Factory.StartNew(() =>
            {
                response = investmentDto.SaveTypeInverter(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt16(typeInvest),
                    Convert.ToInt16(question1),
                    Convert.ToInt16(question2),
                    Convert.ToBoolean(agree)
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="proId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetMarketPlace
        (
            String proId,
            String type
        )
        {
            ResponseMarketPlace response = new ResponseMarketPlace();
            InvestmentDto investmentDto = new InvestmentDto();
            await Task.Factory.StartNew(() =>
            {
                response = investmentDto.GetMarketPlace(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt64(proId),
                    (type != null && !type.Equals("")) ? Convert.ToInt16(type) : 0
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sinId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetSaleInvest
        (
            String sinId
        )
        {
            ResponseSaleInvest response = new ResponseSaleInvest();
            InvestmentDto investmentDto = new InvestmentDto();
            await Task.Factory.StartNew(() =>
            {
                response = investmentDto.GetSaleInvest(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt64(sinId)
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sinId"></param>
        /// <param name="amount"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SaveBuyInvest
        (
            String sinId,
            String amount,
            String fraction,
            String agree
        )
        {
            ResponseMessage response = new ResponseMessage();
            InvestmentDto investmentDto = new InvestmentDto();
            await Task.Factory.StartNew(() =>
            {
                response = investmentDto.SaveBuyInvest(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt64(sinId),
                    Convert.ToInt64(amount),
                    Convert.ToDouble(fraction),
                    (agree=="1") ? true : false
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // View Sale Invest

        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Sale()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _Sale()
        {
            return PartialView("Sale");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [SessionVerify]
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult BuyInvest()
        {
            return View();
        }
        [CompressFilter(Order = 1)]
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult _BuyInvest()
        {
            return PartialView("BuyInvest");
        }

    }

}