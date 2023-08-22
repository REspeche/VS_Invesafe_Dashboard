using BusinessLayer.Classes;
using Resource;
using System;
using System.Linq;

namespace BusinessLayer.DTO
{
    public class SettingDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliId"></param>
        /// <returns></returns>
        public ResponseNotifications GetNotifications(long? cliId)
        {
            ResponseNotifications response = new ResponseNotifications();
            try
            {
                response.items = (from objNot in bdContext.SP_GET_NOTIFICATIONS(cliId)
                                  select new NotificationItem()
                                  {
                                      id = objNot.not_id,
                                      name = objNot.not_name,
                                      enable = objNot.nxc_enable
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
        /// <param name="cliId"></param>
        /// <param name="requestNotifications"></param>
        /// <returns></returns>
        public ResponseMessage SaveNotifications(
            long cliId, 
            RequestNotifications requestNotifications
            )
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                foreach (NotificationItem item in requestNotifications.items)
                {
                    nxc_notxcli objNxc = bdContext.nxc_notxcli.FirstOrDefault((n) => n.cli_id == cliId && n.not_id == item.id);
                    if (objNxc != null)
                    {
                        objNxc.nxc_enable = item.enable;
                    }
                    else
                    {
                        objNxc = new nxc_notxcli();
                        objNxc.cli_id = cliId;
                        objNxc.not_id = item.id;
                        objNxc.nxc_enable = item.enable;
                        bdContext.nxc_notxcli.Add(objNxc);
                    }
                }
                bdContext.SaveChanges();
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
