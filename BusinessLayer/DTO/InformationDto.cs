using BusinessLayer.Classes;
using BusinessLayer.Helpers;
using System;
using System.Linq;

namespace BusinessLayer.DTO
{
    public class InformationDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseReports GetReports()
        {
            ResponseReports response = new ResponseReports();
            try
            {
                response.items = (from objRep in bdContext.SP_GET_REPORTS()
                                 select new ReportItem()
                                 {
                                     id = objRep.rep_id,
                                     title = objRep.rep_title,
                                     image = objRep.rep_image,
                                     file = objRep.rep_file,
                                     path = String.Concat(
                                             CommonHelper.getStoragePath(),
                                             CommonHelper.getContainerName(5)
                                            )
                                 }).ToList();
                response.code = 0;
            }
            catch (Exception ex)
            {
                response.code = -1;
                response.message = ex.Message;
            }
            return response;
        }
        
    }
}
