using BusinessLayer.Classes;
using BusinessLayer.Helpers;
using Resource;
using System;
using System.Linq;
using static BusinessLayer.Enum.Tables;

namespace BusinessLayer.DTO
{
    public class InvestmentDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliId"></param>
        /// <param name="search"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ResponseMyInvestment GetMyInvestments(
            long? cliId,
            string search,
            int type
        )
        {
            ResponseMyInvestment response = new ResponseMyInvestment();
            try
            {
                response.items = (from objInv in bdContext.SP_GET_MYINVESTMENTS(
                                    cliId,
                                    search,
                                    type
                                 )
                                 select new InvestmentItem()
                                 {
                                     id = objInv.pxc_id,
                                     name = objInv.pro_name,
                                     type = objInv.tpr_name,
                                     payment = Resource.Enum.ResourceManager.GetString(paymentMethod.GetName(typeof(paymentMethod), objInv.tpa_id).ToString()).ToString(),
                                     amount = objInv.pxc_amount,
                                     fraction = objInv.frv_value ?? 0,
                                     date = Commons.DateTimeToUnixTimestamp(objInv.pxc_dateCreate),
                                     amountSin = objInv.amountSin ?? 0,
                                     fractionSin = objInv.fractionSin ?? 0,
                                     finish = objInv.pro_finish,
                                     isSold = objInv.isSold ?? false
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
        /// <param name="pxcId"></param>
        /// <returns></returns>
        public ResponseMyInvest GetMyInvest(
            long? cliId,
            long pxcId
        )
        {
            ResponseMyInvest response = new ResponseMyInvest();
            try
            {
                response = (from objInv in bdContext.SP_GET_MYINVEST(
                                cliId,
                                pxcId
                            )
                            select new ResponseMyInvest()
                            {
                                id = objInv.pxc_id,
                                amount = objInv.pxc_amount,
                                idSin = objInv.sin_id ?? 0,
                                amountSin = objInv.sin_amount ?? 1,
                                namePro = objInv.pro_name,
                                idPro = objInv.pro_id,
                                fractionValue = objInv.frv_value ?? 0
                            }).First();
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
        /// <param name="sinId"></param>
        /// <returns></returns>
        public ResponseSaleInvest GetSaleInvest(
            long cliId,
            long sinId
        )
        {
            ResponseSaleInvest response = new ResponseSaleInvest();
            try
            {
                response = (from objInv in bdContext.SP_GET_SALE_INVEST(
                                cliId,
                                sinId
                            )
                            select new ResponseSaleInvest()
                            {
                                id = objInv.pxc_id,
                                amount = objInv.pxc_amount,
                                idSin = objInv.sin_id ?? 0,
                                amountSin = objInv.sin_amount ?? 0,
                                fractionSin = objInv.sin_fraction ?? 0,
                                namePro = objInv.pro_name,
                                idPro = objInv.pro_id,
                                profitability = objInv.pro_profitability,
                                timeLimit = objInv.pro_timeLimit
                            }).First();
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
        /// <returns></returns>
        public ResponseTypeInverter GetTypeInverter(
            long? cliId
        )
        {
            ResponseTypeInverter response = new ResponseTypeInverter();
            try
            {
                response = (from objInv in bdContext.SP_GET_TYPEINVERTER(
                                cliId
                            )
                            select new ResponseTypeInverter()
                            {
                                typeInvest = objInv.typeInvest ?? 0,
                                question1 = objInv.question1 ?? 0,
                                question2 = objInv.question2 ?? 0,
                                agree = objInv.agree
                            }).FirstOrDefault();
                if (response==null)
                {
                    response = new ResponseTypeInverter();
                    response.typeInvest = 0;
                    response.question1 = 0;
                    response.question2 = 0;
                    response.agree = false;
                }
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
        /// <param name="typeInvest"></param>
        /// <param name="question1"></param>
        /// <param name="question2"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        public ResponseMessage SaveTypeInverter(
            long cliId,
            int tInvest,
            int question1,
            int question2,
            bool agree
        )
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                aca_accreditAccount objAca = bdContext.aca_accreditAccount.FirstOrDefault((a) => a.cli_id == cliId);
                if (objAca == null)
                {
                    objAca = new aca_accreditAccount();
                    objAca.cli_id = cliId;
                    objAca.tin_typeInvest = tInvest;
                    objAca.aca_question1 = (tInvest == (int)typeInvest.No_Acreditado) ? 0 : question1;
                    objAca.aca_question2 = (tInvest == (int)typeInvest.No_Acreditado) ? 0 : question2;
                    objAca.aca_agree = (tInvest == (int)typeInvest.No_Acreditado) ? false : agree;
                    objAca.aca_dateCreate = DateTime.Now;
                    objAca.aca_enable = true;
                    bdContext.aca_accreditAccount.Add(objAca);
                }
                else
                {
                    objAca.tin_typeInvest = tInvest;
                    objAca.aca_question1 = (tInvest == (int)typeInvest.No_Acreditado) ? 0 : question1;
                    objAca.aca_question2 = (tInvest == (int)typeInvest.No_Acreditado) ? 0 : question2;
                    objAca.aca_agree = (tInvest == (int)typeInvest.No_Acreditado) ? false : agree;
                    objAca.aca_enable = true;
                }
                bdContext.SaveChanges();

                // Save alerts
                AlertDto alertDto = new AlertDto();
                alertDto.ChangeState(cliId, 5, alertState.Approved);
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
        /// <param name="pxcId"></param>
        /// <param name="cliId"></param>
        /// <param name="amount"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        public ResponseMessage SaveSale(
            long pxcId,
            long cliId,
            long amount,
            double fraction,
            yesNo agree
        )
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                if (agree == yesNo.Yes)
                {
                    pxc_proxcli objPxc = bdContext.pxc_proxcli.FirstOrDefault((p) => p.pxc_id == pxcId && p.cli_id == cliId);
                    if (objPxc != null)
                    {
                        sin_saleInvest objSin = bdContext.sin_saleInvest.FirstOrDefault((s) => s.pxc_id == pxcId);
                        if (objSin != null)
                        {
                            objSin.sin_amount = amount;
                            objSin.sin_fraction = Math.Round(fraction, 2);
                            objSin.sin_enable = true;
                        }
                        else
                        {
                            objSin = new sin_saleInvest();
                            objSin.pxc_id = objPxc.pxc_id;
                            objSin.sin_amount = amount;
                            objSin.sin_fraction = Math.Round(fraction, 2);
                            objSin.sin_dateCreate = DateTime.UtcNow;
                            objSin.sin_enable = true;
                            bdContext.sin_saleInvest.Add(objSin);
                        }

                        bdContext.SaveChanges();
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
        /// <param name="pxcId"></param>
        /// <param name="cliId"></param>
        /// <param name="sinId"></param>
        /// <returns></returns>
        public ResponseMessage RemoveSale(
            long pxcId,
            long cliId,
            long sinId
        )
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                pxc_proxcli objPxc = bdContext.pxc_proxcli.FirstOrDefault((p) => p.pxc_id == pxcId && p.cli_id == cliId);
                if (objPxc != null)
                {
                    sin_saleInvest objSin = bdContext.sin_saleInvest.FirstOrDefault((s) => s.pxc_id == pxcId);
                    if (objSin != null)
                    {
                        objSin.sin_enable = false;
                        bdContext.SaveChanges();
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
        /// <param name="sinId"></param>
        /// <param name="amount"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        public ResponseMessage SaveBuyInvest(
            long cliId,
            long sinId,
            long amount,
            double fraction,
            bool agree
        )
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                sin_saleInvest objSin = bdContext.sin_saleInvest.FirstOrDefault((s) => s.sin_id == sinId);
                if (objSin != null)
                {
                    objSin.sin_amount = amount;
                    objSin.sin_fraction = Math.Round(fraction, 2);
                    objSin.sin_sold = true;
                    objSin.cli_idSold = cliId;

                    pxc_proxcli objPxc = bdContext.pxc_proxcli.FirstOrDefault((p) => p.pxc_id == objSin.pxc_id);
                    top_typeOperation objTop = bdContext.top_typeOperation.FirstOrDefault((t) => t.top_id == (int)typeOperation.Venta_fracciones && t.top_enable == true);
                    if (objPxc != null && objTop != null)
                    {
                        /* Registrar el movimiento al inversor que vendio */
                        mov_movement objMov = new mov_movement();
                        objMov.cli_id = objPxc.cli_id;
                        objMov.top_id = objTop.top_id;
                        objMov.mov_amount = amount;
                        objMov.mov_inputOutput = objTop.top_inputOutput;
                        objMov.mov_dateCreate = DateTime.UtcNow;
                        objMov.mov_validate = true;
                        bdContext.mov_movement.Add(objMov);

                        objPxc.pxc_enable = false; // anular la inversion del que realizo la venta para despues acreditarle la diferencia

                        /* Acreditarla diferencia al inversor que realizo la venta */
                        pxc_proxcli objPxc4 = new pxc_proxcli();
                        objPxc4.pro_id = objPxc.pro_id;
                        objPxc4.cli_id = objPxc.cli_id;
                        objPxc4.pxc_amount = (objPxc.pxc_amount - amount);
                        objPxc4.tpa_id = (int)Enum.Tables.paymentMethod.Account;
                        objPxc4.pxc_dateCreate = objPxc.pxc_dateCreate; //la fecha de la primera inversion
                        objPxc4.pxc_enable = true;
                        bdContext.pxc_proxcli.Add(objPxc4);

                        /* Acreditar el monto a la cuenta del nuevo inversor */
                        pxc_proxcli objPxc3 = new pxc_proxcli();
                        objPxc3.pro_id = objPxc.pro_id;
                        objPxc3.cli_id = cliId;
                        objPxc3.pxc_amount = amount;
                        objPxc3.tpa_id = (int)Enum.Tables.paymentMethod.Account;
                        objPxc3.pxc_dateCreate = DateTime.UtcNow;
                        objPxc3.pxc_enable = true;
                        bdContext.pxc_proxcli.Add(objPxc3);
                    }

                    /* Registrar el movimiento del nuevo inversor */
                    mov_movement objMov2 = new mov_movement();
                    objMov2.cli_id = cliId;
                    objMov2.top_id = (int)typeOperation.Compra_fracciones;
                    objMov2.mov_amount = amount;
                    objMov2.mov_inputOutput = (int)ioMovement.Output;
                    objMov2.mov_dateCreate = DateTime.UtcNow;
                    objMov2.mov_validate = true;
                    bdContext.mov_movement.Add(objMov2);

                    bdContext.SaveChanges();
                }
                else
                {
                    response.code = 302;
                    response.message = Resource.Messages.Error.errorProcedure;
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
        /// <param name="cliId"></param>
        /// <param name="proId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ResponseMarketPlace GetMarketPlace(
            long? cliId,
            long proId,
            int type
        )
        {
            ResponseMarketPlace response = new ResponseMarketPlace();
            try
            {
                response.items = (from objSin in bdContext.SP_GET_MARKETPLACE(
                                    cliId,
                                    proId,
                                    type
                                 )
                                 select new MarketPlaceItem()
                                 {
                                     id = objSin.sin_id,
                                     nameProject = objSin.pro_name,
                                     typeProject = objSin.tpr_name,
                                     amount = objSin.sin_amount ?? 0,
                                     fraction = objSin.sin_fraction ?? 0,
                                     actualFraction = objSin.actualFraction ?? 0,
                                     date = Commons.DateTimeToUnixTimestamp(objSin.sin_dateCreate),
                                     canBuy = (objSin.canBuy==1)?true:false
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
