using BusinessLayer.Classes;
using BusinessLayer.Helpers;
using Resource;
using System;
using System.Linq;
using static BusinessLayer.Enum.Tables;

namespace BusinessLayer.DTO
{
    public class ProjectDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliId"></param>
        /// <returns></returns>
        public ResponseProjects GetProjects(long? cliId, int countRecords)
        {
            ResponseProjects response = new ResponseProjects();
            try
            {
                response.items =    (from objPro in bdContext.SP_GET_PROJECTS(cliId)
                                    select new ProjectItem()
                                    {
                                        id = objPro.pro_id,
                                        name = objPro.pro_name,
                                        subName = objPro.pro_subName,
                                        type = objPro.pro_type,
                                        status = objPro.pro_status,
                                        city = objPro.pro_city,
                                        profitability = objPro.pro_profitability,
                                        timeLimit = objPro.pro_timeLimit,
                                        goalDays = objPro.pro_goalDays,
                                        finish = objPro.pro_finish,
                                        countClients = objPro.countClients ?? 0,
                                        historicTIR = objPro.pro_historicTIR,
                                        daysRuning = objPro.daysRuning ?? 0,
                                        goalAmount = objPro.pro_goalAmount,
                                        totalInvested = objPro.totalInvested ?? 0,
                                        percentInvested = Math.Round(((objPro.totalInvested ?? 0) * 100) / objPro.pro_goalAmount, 2),
                                        icon = objPro.tpr_icon,
                                        finishInvest = objPro.finishInvest ?? false
                                    }).Take((countRecords==0) ? 999 : countRecords).ToList();
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
        /// <param name="proId"></param>
        /// <returns></returns>
        public ResponseProject GetProject(
            long? cliId,
            long proId
        )
        {
            ResponseProject response = new ResponseProject();
            try
            {
                response =      (from objPro in bdContext.SP_GET_PROJECT(cliId, proId)
                                 select new ResponseProject()
                                 {
                                     id = objPro.pro_id,
                                     name = objPro.pro_name,
                                     subName = objPro.pro_subName,
                                     typeId = objPro.pro_typeId,
                                     typeName = objPro.pro_typeName,
                                     status = objPro.pro_status,
                                     city = objPro.pro_city,
                                     profitability = objPro.pro_profitability,
                                     timeLimit = objPro.pro_timeLimit,
                                     goalDays = objPro.pro_goalDays,
                                     finish = objPro.pro_finish,
                                     countClients = objPro.countClients ?? 0,
                                     historicTIR = objPro.pro_historicTIR,
                                     daysRuning = objPro.daysRuning ?? 0,
                                     goalAmount = objPro.pro_goalAmount,
                                     totalInvested = objPro.totalInvested ?? 0,
                                     percentInvested = Math.Round(((objPro.totalInvested ?? 0) * 100) / objPro.pro_goalAmount, 2),
                                     icon = objPro.tpr_icon,
                                     purchasePrice = objPro.pro_purchasePrice ?? 0,
                                     price1 = objPro.pro_price1 ?? 0,
                                     price2 = objPro.pro_price2 ?? 0,
                                     price3 = objPro.pro_price3 ?? 0,
                                     price4 = objPro.pro_price4 ?? 0,
                                     priceTotal = objPro.pro_priceTotal ?? 0,
                                     startFinancing = Commons.DateTimeToUnixTimestamp(objPro.pro_startFinancing),
                                     endFinancing = (objPro.endFinancing != null) ? Commons.DateTimeToUnixTimestamp(objPro.endFinancing) : 0,
                                     minInvest = objPro.minInvest,
                                     maxInvest = objPro.maxInvest ?? 0,
                                     maxInvestTransfer = Convert.ToDouble(CommonHelper.getParamCache().maxToInvestByTransfer),
                                     fractionInitial = objPro.fractionInitial ?? 1,
                                     fractionActual = objPro.fractionActual ?? 1,
                                     finishInvest = objPro.finishInvest ?? false,
                                     restAmount = objPro.pro_goalAmount - objPro.totalInvested ?? 0
                                 }).FirstOrDefault();
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
        /// <param name="proId"></param>
        /// <returns></returns>
        public ResponseDocuments GetDocuments(
            long? cliId,
            long proId
        )
        {
            ResponseDocuments response = new ResponseDocuments();
            try
            {
                response.items = (from objDoc in bdContext.SP_GET_DOCUMENTS(cliId, proId)
                                 select new DocumentItem()
                                 {
                                     cadId = objDoc.cad_id,
                                     name = objDoc.dbp_name,
                                     file = objDoc.dbp_file
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
        /// <param name="proId"></param>
        /// <returns></returns>
        public ResponseDocuments GetDocumentsSite(
            long? cliId,
            long proId
        )
        {
            ResponseDocuments response = new ResponseDocuments();
            try
            {
                response.items = (from objDoc in bdContext.SP_GET_DOCUMENTS(cliId, proId)
                                  select new DocumentItem()
                                  {
                                      cadId = objDoc.cad_id,
                                      name = objDoc.dbp_name,
                                      file = null
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
        /// <param name="gender"></param>
        /// <param name="civilState"></param>
        /// <param name="profession"></param>
        /// <param name="annualIncome"></param>
        /// <returns></returns>
        public ResponseMessage SaveInvest(
            long cliId,
            long proId,
            long amount,
            paymentMethod payment,
            yesNo agree,
            string code
        )
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                if (agree == yesNo.Yes)
                {
                    double maxTransfer = Convert.ToDouble(CommonHelper.getParamCache().maxToInvestByTransfer);

                    // Monto mayor a lo permitido para transferencia bancaria
                    if (amount > maxTransfer && paymentMethod.Card.Equals(payment))
                    {
                        response.code = 206;
                        response.message = Resource.Messages.Warning.errorAmountTransfer;
                        return response;
                    }

                    cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == cliId);
                    if (objCli != null)
                    {
                        pro_project objPro = bdContext.pro_project.FirstOrDefault((p) => p.pro_id == proId);
                        if (objPro != null)
                        {
                            ResponseProject resPro = GetProject(cliId, proId);
                            double restAmount = resPro.goalAmount - resPro.totalInvested;
                            if (restAmount >= amount)
                            {
                                pxc_proxcli objPxc = new pxc_proxcli();
                                objPxc.pro_id = objPro.pro_id;
                                objPxc.cli_id = objCli.cli_id;
                                objPxc.pxc_amount = amount;
                                objPxc.tpa_id = (int)payment;
                                objPxc.pxc_code = code;
                                objPxc.pxc_dateCreate = DateTime.UtcNow;
                                objPxc.pxc_enable = true;
                                bdContext.pxc_proxcli.Add(objPxc);

                                top_typeOperation objTop = bdContext.top_typeOperation.FirstOrDefault((t) => t.top_id == (int)typeOperation.Inversion && t.top_enable == true);
                                if (payment == paymentMethod.Account && objTop != null)
                                {
                                    mov_movement objMov = new mov_movement();
                                    objMov.cli_id = objCli.cli_id;
                                    objMov.top_id = objTop.top_id;
                                    objMov.mov_amount = amount;
                                    objMov.mov_inputOutput = objTop.top_inputOutput;
                                    objMov.mov_dateCreate = DateTime.UtcNow;
                                    objMov.mov_validate = false;
                                    bdContext.mov_movement.Add(objMov);
                                }

                                bdContext.SaveChanges();
                            }
                            else
                            {
                                response.code = 201;
                                response.message = String.Format(Resource.Messages.Warning.amountFail.ToString(), restAmount);
                            }
                        }
                        else
                        {
                            response.code = 302;
                            response.message = Resource.Messages.Error.errorProcedure;
                        }
                    }
                    else
                    {
                        response.code = 302;
                        response.message = Resource.Messages.Error.errorProcedure;
                    }
                }
                else
                {
                    response.code = 200;
                    response.message = Resource.Messages.Warning.aceptAgree;
                }
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
        public ResponseItemCombo GetListProjects()
        {
            ResponseItemCombo response = new ResponseItemCombo();

            try
            {
                response.items = (from pro in bdContext.pro_project.AsEnumerable()
                                 where pro.pro_enable == true
                                 orderby pro.pro_id ascending
                                 select new ComboItem()
                                 {
                                     id = pro.pro_id,
                                     label = pro.pro_name
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
