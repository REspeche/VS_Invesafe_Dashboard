using BusinessLayer.Classes;
using BusinessLayer.Enum;
using Resource;
using System;
using System.Linq;
using static BusinessLayer.Enum.Tables;

namespace BusinessLayer.DTO
{
    public class AlertDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseAlerts GetAlerts(long id)
        {
            ResponseAlerts response = new ResponseAlerts();
            try
            {
                response.items = (from objAle in bdContext.SP_GET_ALERTS(id)
                                  select new AlertItem()
                                  {
                                      id = objAle.ale_id,
                                      groupName = objAle.agr_name,
                                      mesage = (objAle.axc_state == (int)Tables.alertState.Pendient) ? objAle.ale_message_pending : (objAle.axc_state == (int)Tables.alertState.Sent) ? objAle.ale_message_validating : (objAle.axc_state == (int)Tables.alertState.Rejected) ? objAle.ale_message_rejected : objAle.ale_message_approved,
                                      link = objAle.ale_link,
                                      state = objAle.axc_state ?? (int)Tables.alertState.Pendient
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
        /// <param name="idCli"></param>
        /// <param name="idAle"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ResponseMessage ChangeState(long idCli, long idAle, alertState state)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                axc_alexcli objAxc = bdContext.axc_alexcli.FirstOrDefault((a) => a.cli_id == idCli && a.ale_id == idAle);
                if (objAxc != null)
                {
                    objAxc.axc_state = (int)state;
                    objAxc.axc_visible = true;
                    bdContext.SaveChanges();
                }

                response.code = 0;
                response.message = Resource.Messages.Success.notifyOK;
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
        /// <param name="idCli"></param>
        /// <param name="listAle"></param>
        /// <returns></returns>
        public ResponseMessage CreateAlerts(long idCli, long[] listAle)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                foreach (int idAle in listAle)
                {
                    axc_alexcli objAxc = new axc_alexcli();
                    objAxc.cli_id = idCli;
                    objAxc.ale_id = idAle;
                    objAxc.axc_state = (int)Tables.alertState.Pendient;
                    objAxc.axc_visible = true;
                    bdContext.axc_alexcli.Add(objAxc);
                    bdContext.SaveChanges();
                }

                response.code = 0;
                response.message = Resource.Messages.Success.notifyOK;
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
        /// <param name="idCli"></param>
        /// <param name="idAle"></param>
        /// <returns></returns>
        public ResponseAlerts GetAlertById(long idCli, long[] listAle)
        {
            ResponseAlerts response = new ResponseAlerts();
            try
            {
                response.items = (from objAle in bdContext.SP_GET_ALERTS(idCli)
                                 where listAle.Contains(objAle.ale_id)
                                 select new AlertItem()
                                 {
                                     id = objAle.ale_id,
                                     groupName = objAle.agr_name,
                                     mesage = (objAle.axc_state == (int)Tables.alertState.Pendient) ? objAle.ale_message_pending : (objAle.axc_state == (int)Tables.alertState.Sent) ? objAle.ale_message_validating : (objAle.axc_state == (int)Tables.alertState.Rejected) ? objAle.ale_message_rejected : objAle.ale_message_approved,
                                     link = objAle.ale_link,
                                     state = objAle.axc_state ?? (int)Tables.alertState.Pendient
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
