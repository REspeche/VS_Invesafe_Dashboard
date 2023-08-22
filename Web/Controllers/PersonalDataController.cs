using BusinessLayer.Classes;
using BusinessLayer.DTO;
using BusinessLayer.Helpers;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Filters;
using Web.Helpers;

namespace Web.Controllers
{
    public class PersonalDataController : BaseController
    {
        /// <summary>
        /// Returns personal Data from client
        /// </summary>
        /// <param name="email"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetPersonalData()
        {
            ResponsePersonalData response = new ResponsePersonalData();
            PersonalDataDto personalDataDto = new PersonalDataDto();
            await Task.Factory.StartNew(() =>
            {
                response = personalDataDto.GetPersonalData(
                    Convert.ToInt64(Session["id"]),
                    Session["email"].ToString()
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="codePhone"></param>
        /// <param name="phone"></param>
        /// <param name="bornDate"></param>
        /// <param name="nationality"></param>
        /// <param name="live"></param>
        /// <param name="city"></param>
        /// <param name="cp"></param>
        /// <param name="address"></param>
        /// <param name="dni"></param>
        /// <param name="exposedPolitician"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SavePersonalData(
            string firstName,
            string lastName,
            string email,
            string codePhone,
            string phone,
            string bornDate,
            string nationality,
            string live,
            string city,
            string cp,
            string address,
            string cuit,
            string exposedPolitician
            )
        {
            ResponsePersonalData response = new ResponsePersonalData();
            PersonalDataDto personalDataDto = new PersonalDataDto();
            await Task.Factory.StartNew(() =>
            {
                response = personalDataDto.SavePersonalData(
                    Convert.ToInt64(Session["id"]),
                    firstName,
                    lastName,
                    email,
                    codePhone,
                    phone,
                    Convert.ToDouble(bornDate),
                    Convert.ToInt16(nationality),
                    Convert.ToInt16(live),
                    city,
                    cp,
                    address,
                    cuit,
                    Convert.ToInt16(exposedPolitician)
                    );

                if (response.code == 0)
                {
                    Session["fullName"] = String.Concat(response.firstName, " ", response.lastName);
                }
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SaveDocuments()
        {
            HttpPostedFileBase[] files = new HttpPostedFileBase[Request.Files.Count];
            for (int i = 0; i < Request.Files.Count; i++)
            {
                files[i] = Request.Files[i];
            }
            ResponsePersonalData response = new ResponsePersonalData();
            ReturnUpload returnUpload = UploadBlobHelper.UploadFiles(files);

            await Task.Factory.StartNew(() =>
            {
                if (returnUpload.code == 0 && returnUpload.files.Count == 2)
                {
                    CommonDto commonDto = new CommonDto();

                    PersonalDataDto personalDataDto = new PersonalDataDto();
                    ResponseMessage responseDocument = personalDataDto.UpdateDocumentFront(
                        Convert.ToInt64(Session["id"]),
                        Session["email"].ToString(),
                        commonDto.InsertBlob(
                                    returnUpload.files[0].newGuid,
                                    returnUpload.files[0].fileName,
                                    returnUpload.files[0].fileExtension,
                                    returnUpload.files[0].fileWidth,
                                    returnUpload.files[0].fileHeight,
                                    returnUpload.files[0].container
                                    )
                        );

                    if (responseDocument.code == 0)
                    {
                        response.photoDocumentFront = String.Concat(CommonHelper.getStoragePath(), "images/", returnUpload.files[0].newGuid.ToString().ToUpper(), ".", returnUpload.files[0].fileExtension);
                    }
                    else
                    {
                        response.code = 305;
                        response.message = Resource.Messages.Error.errorUploadDocumentFront;
                    }

                    if (responseDocument.code == 0)
                    {
                        responseDocument = personalDataDto.UpdateDocumentBack(
                            Convert.ToInt64(Session["id"]),
                            Session["email"].ToString(),
                            commonDto.InsertBlob(
                                        returnUpload.files[1].newGuid,
                                        returnUpload.files[1].fileName,
                                        returnUpload.files[1].fileExtension,
                                        returnUpload.files[1].fileWidth,
                                        returnUpload.files[1].fileHeight,
                                        returnUpload.files[1].container
                                        )
                            );

                        if (responseDocument.code == 0)
                        {
                            response.photoDocumentBack = String.Concat(CommonHelper.getStoragePath(), "images/", returnUpload.files[1].newGuid.ToString().ToUpper(), ".", returnUpload.files[1].fileExtension);
                        }
                        else
                        {
                            response.code = 304;
                            response.message = Resource.Messages.Error.errorUploadDocumentBack;
                        }
                    }
                }
                else
                {
                    response.code = 306;
                    response.message = Resource.Messages.Error.errorUploadDocuments;
                }
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> GetAditionalData()
        {
            ResponseAditionalData response = new ResponseAditionalData();
            PersonalDataDto personalDataDto = new PersonalDataDto();
            await Task.Factory.StartNew(() =>
            {
                response = personalDataDto.GetAditionalData(
                        Convert.ToInt64(Session["id"]),
                        Session["email"].ToString()
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gender"></param>
        /// <param name="civilState"></param>
        /// <param name="profession"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequest]
        [CompressFilter(Order = 1)]
        public async Task<JsonResult> SaveAditionalData(
            string gender,
            string civilState,
            string profession,
            string annualIncome
        )
        {
            ResponseAditionalData response = new ResponseAditionalData();
            PersonalDataDto personalDataDto = new PersonalDataDto();
            await Task.Factory.StartNew(() =>
            {
                response = personalDataDto.SaveAditionalData(
                    Convert.ToInt64(Session["id"]),
                    Convert.ToInt16(gender),
                    Convert.ToInt16(civilState),
                    profession,
                    Convert.ToDouble(annualIncome)
                    );
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}