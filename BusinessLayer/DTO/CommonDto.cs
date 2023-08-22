using BusinessLayer.Classes;
using BusinessLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.DTO
{
    public class CommonDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// Gets the list country.
        /// </summary>
        /// <returns></returns>
        public ResponseItemCombo GetListCountry()
        {
            ResponseItemCombo response = new ResponseItemCombo();

            try
            {
                response.items = (from cou in bdContext.cou_country.AsEnumerable()
                                 where cou.cou_enable == true && cou.cou_enable == true
                                 orderby cou.cou_countryName ascending
                                 select new ComboItem()
                                 {
                                     id = cou.cou_id,
                                     label = cou.cou_countryName
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseItemCombo GetListYesNo()
        {
            ResponseItemCombo response = new ResponseItemCombo();

            try
            {
                response.items = (new ComboItem[] {
                    new ComboItem
                    {
                        id = 0,
                        label = "No"
                    },
                    new ComboItem
                    {
                        id = 1,
                        label = "Si"
                    }
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

        /// <summary>
        /// Inserts the BLOB.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="container">The container.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public long InsertBlob(Guid guid, String fileName, String extension, int width, int height, int container)
        {
            long idRes = 0;
            try
            {
                blo_blob objBlo = new blo_blob();
                objBlo.blo_guid = guid;
                objBlo.blo_filename = fileName;
                objBlo.blo_extension = extension;
                objBlo.blo_container = container;
                objBlo.blo_width = width;
                objBlo.blo_height = height;
                objBlo.blo_dateCreate = DateTime.UtcNow;
                bdContext.blo_blob.Add(objBlo);
                bdContext.SaveChanges();

                idRes = objBlo.blo_id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                idRes = -1;
            }
            return idRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseItemCombo GetListTypeAccounts()
        {
            ResponseItemCombo response = new ResponseItemCombo();

            try
            {
                response.items = (from tac in bdContext.tac_typeAccount.AsEnumerable()
                                 where tac.tac_enable == true
                                 orderby tac.tac_id ascending
                                 select new ComboItem()
                                 {
                                     id = tac.tac_id,
                                     label = tac.tac_name
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseItemCombo GetListTypeOperations()
        {
            ResponseItemCombo response = new ResponseItemCombo();

            try
            {
                response.items = (from top in bdContext.top_typeOperation.AsEnumerable()
                                 where top.top_enable == true
                                 orderby top.top_id ascending
                                 select new ComboItem()
                                 {
                                     id = top.top_id,
                                     label = top.top_name
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseItemCombo GetListTypeProjects()
        {
            ResponseItemCombo response = new ResponseItemCombo();

            try
            {
                response.items = (from tpr in bdContext.tpr_typeProject.AsEnumerable()
                                 where tpr.tpr_enable == true
                                 orderby tpr.tpr_id ascending
                                 select new ComboItem()
                                 {
                                     id = tpr.tpr_id,
                                     label = tpr.tpr_name
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

        public CacheParams GetParameters()
        {
            CacheParams response = new CacheParams();
            try
            {
                List<app_appParameters> recApp = (from app in bdContext.app_appParameters.AsEnumerable()
                                                  select app).ToList();
                foreach (var item in recApp)
                {
                    typeof(CacheParams).GetProperty("_"+item.app_key.TrimEnd()).SetValue(null, item.app_value);
                }

            }
            catch(Exception)
            {
                throw;
            }
            return response;
        }
    }
}
