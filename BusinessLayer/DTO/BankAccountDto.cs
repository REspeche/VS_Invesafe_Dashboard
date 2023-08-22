using BusinessLayer.Classes;
using BusinessLayer.Helpers;
using Resource;
using System;
using System.Linq;
using System.Resources;
using static BusinessLayer.Enum.Tables;

namespace BusinessLayer.DTO
{
    public class BankAccountDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliId"></param>
        /// <returns></returns>
        public ResponseBankAccounts GetBankAccounts(long? cliId)
        {
            ResponseBankAccounts response = new ResponseBankAccounts();
            try
            {
                response.items = (from objAcc in bdContext.SP_GET_BANKACCOUNTS(cliId)
                                 select new BankAccountItem()
                                 {
                                     id = objAcc.ban_id,
                                     tacId = objAcc.tac_id ?? 0,
                                     holder = objAcc.ban_nameTitular,
                                     accountNbr = objAcc.ban_accountNumber,
                                     cbu = objAcc.ban_cbu,
                                     bankName = objAcc.ban_bank,
                                     state = objAcc.ban_state ?? 1,
                                     fileDocument = String.Concat(
                                         CommonHelper.getStoragePath(),
                                         CommonHelper.getContainerName((int)objAcc.blo_container),
                                         objAcc.guidName
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliId"></param>
        /// <returns></returns>
        public ResponseBankAccounts GetBankAccountsValidated(long? cliId)
        {
            ResponseBankAccounts response = new ResponseBankAccounts();
            try
            {
                response.items = (from objAcc in bdContext.SP_GET_BANKACCOUNTS_VALIDATED(cliId)
                                  select new BankAccountItem()
                                  {
                                      id = objAcc.ban_id,
                                      tacId = objAcc.tac_id ?? 0,
                                      holder = objAcc.ban_nameTitular,
                                      accountNbr = objAcc.ban_accountNumber,
                                      cbu = objAcc.ban_cbu,
                                      bankName = objAcc.ban_bank,
                                      state = objAcc.ban_state ?? 1,
                                      fileDocument = String.Concat(
                                          CommonHelper.getStoragePath(),
                                          CommonHelper.getContainerName((int)objAcc.blo_container),
                                          objAcc.guidName
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliId"></param>
        /// <param name="tacId"></param>
        /// <param name="holder"></param>
        /// <param name="accountNbr"></param>
        /// <param name="bankName"></param>
        /// <param name="cbu"></param>
        /// <returns></returns>
        public ResponseMessage SaveAddAccount(
            long cliId,
            int tacId,
            string holder,
            string accountNbr,
            string bankName,
            string cbu,
            long? idBlob
         )
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                ban_bankAccount objAcc = bdContext.ban_bankAccount.FirstOrDefault((a) => a.cli_id == cliId && a.ban_accountNumber == accountNbr && a.ban_enable == true);
                if (objAcc == null)
                {
                    objAcc = new ban_bankAccount();
                    objAcc.cli_id = cliId;
                    objAcc.tac_id = tacId;
                    objAcc.blo_idDocument = idBlob;
                    objAcc.ban_nameTitular = holder;
                    objAcc.ban_accountNumber = accountNbr;
                    objAcc.ban_cbu = cbu;
                    objAcc.ban_bank = bankName;
                    objAcc.ban_default = false;
                    objAcc.ban_state = 2;
                    objAcc.ban_dateCreate = DateTime.Now;
                    objAcc.ban_enable = true;
                    bdContext.ban_bankAccount.Add(objAcc);
                    bdContext.SaveChanges();

                    // Save alerts
                    AlertDto alertDto = new AlertDto();
                    alertDto.ChangeState(cliId, 4, alertState.Sent);
                }
                else
                {
                    response.code = 202;
                    response.message = Resource.Messages.Warning.errorAccountExist;
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
        /// <param name="banId"></param>
        /// <returns></returns>
        public ResponseMessage RemoveAccount(
            long cliId,
            long banId
            )
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                ban_bankAccount objAcc = bdContext.ban_bankAccount.FirstOrDefault((a) => a.cli_id == cliId && a.ban_id == banId && a.ban_state!=2);
                if (objAcc != null)
                {
                    objAcc.ban_enable = false;
                    bdContext.SaveChanges();

                    var accCount = (from a in bdContext.ban_bankAccount where a.cli_id == cliId select a).ToList().Count;

                    if (accCount == 0) {
                        // Save alerts
                        AlertDto alertDto = new AlertDto();
                        alertDto.ChangeState(cliId, 4, alertState.Pendient);
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
        /// <returns></returns>
        public ResponseReferenceCode GetReferenceCode(long? cliId)
        {
            ResponseReferenceCode response = new ResponseReferenceCode();
            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == cliId);
                if (objCli != null)
                {
                    response.reference = objCli.cli_codeReference;
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

        public ResponseBankMovements GetBankMovements(
            long cliId,
            int topId,
            double startDate,
            double endDate
            )
        {
            ResponseBankMovements response = new ResponseBankMovements();
            try
            {
                response.items = (from objMov in bdContext.SP_GET_MOVEMENTS(
                                    cliId,
                                    topId,
                                    (startDate == 0) ? (DateTime?)null: Commons.UnixTimeStampToDateTime(startDate),
                                    (endDate == 0) ? (DateTime?)null : Commons.UnixTimeStampToDateTime(endDate)
                                    )
                                 select new BankMovementItem()
                                 {
                                     id = objMov.mov_id,
                                     type = objMov.top_name,
                                     amount = objMov.mov_amount,
                                     inputOutput = Resource.Enum.ResourceManager.GetString(ioMovement.GetName(typeof(ioMovement), objMov.mov_inputOutput).ToString()),
                                     concept = objMov.mov_concept,
                                     dateApply = Commons.DateTimeToUnixTimestamp(objMov.mov_dateCreate),
                                     validate = objMov.mov_validate
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
        /// <returns></returns>
        public ResponseBalanceAccount GetBalanceAccount(long? cliId)
        {
            ResponseBalanceAccount response = new ResponseBalanceAccount();
            try
            {
                response = (from objBal in bdContext.SP_GET_BALANCEACCOUNT(cliId)
                            select new ResponseBalanceAccount()
                            {
                                balance = objBal.balanceAccount ?? 0,
                                gain = objBal.gainAccount ?? 0,
                                authenticationMethod = objBal.authenticationMethod ?? 0
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
        /// <param name="returnFull"></param>
        /// <returns></returns>
        public ResponsePatrimonialSituation GetPatrimonialSituation(
            long? cliId,
            bool returnFull
        )
        {
            ResponsePatrimonialSituation response = new ResponsePatrimonialSituation();
            try
            {
                response.item = (from objPat in bdContext.SP_GET_PATRIMONIALSITUATION(cliId, returnFull)
                                select new PatrimonialSituationItem()
                                {
                                    total = objPat.total ?? 0,
                                    available = objPat.available ?? 0,
                                    projects = objPat.projects ?? 0,
                                    invest = objPat.invest ?? 0,
                                    avgInvest = objPat.avgInvest ?? 0,
                                    benefit = objPat.benefit ?? 0,
                                    expenseInvesafe = objPat.expense1 ?? 0,
                                    expenseExtra = objPat.expense2 ?? 0,
                                    expenseLeak = objPat.expense3 ?? 0,
                                    expenseTotal = objPat.expenseTotal ?? 0,
                                    benefit_1 = objPat.benefit1 ?? 0,
                                    benefit_2 = objPat.benefit2 ?? 0,
                                    benefit_3 = objPat.benefit3 ?? 0,
                                    benefit_4 = objPat.benefit4 ?? 0,
                                    benefit_5 = objPat.benefit5 ?? 0,
                                    benefitTotal = objPat.benefitTotal ?? 0,
                                    chartValues = objPat.chartValues
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
        /// <param name="banId"></param>
        /// <param name="ammount"></param>
        /// <param name="concept"></param>
        /// <returns></returns>
        public ResponseMessage RetireFunds(
            long cliId,
            long banId,
            double ammount,
            string concept
         )
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                ban_bankAccount objAcc = bdContext.ban_bankAccount.FirstOrDefault((b) => b.cli_id == cliId && b.ban_id == banId && b.ban_enable == true);
                if (objAcc != null)
                {
                    ResponseBalanceAccount objBal = GetBalanceAccount(cliId);
                    if (ammount <= objBal.gain)
                    {
                        rfu_retireFund objRfu = bdContext.rfu_retireFund.FirstOrDefault((r) => r.cli_id == cliId && r.ban_id == banId && r.rfu_validate == false);
                        top_typeOperation objTop = bdContext.top_typeOperation.FirstOrDefault((t) => t.top_id == (int)typeOperation.Transferencia_Saliente && t.top_enable == true);
                        if (objRfu == null && objTop != null)
                        {
                            objRfu = new rfu_retireFund();
                            objRfu.cli_id = cliId;
                            objRfu.ban_id = banId;
                            objRfu.rfu_ammount = ammount;
                            objRfu.rfu_concept = concept;
                            objRfu.rfu_dateCreate = DateTime.Now;
                            objRfu.rfu_validate = false;
                            bdContext.rfu_retireFund.Add(objRfu);

                            mov_movement objMov = new mov_movement();
                            objMov.cli_id = objRfu.cli_id;
                            objMov.top_id = objTop.top_id;
                            objMov.mov_amount = objRfu.rfu_ammount;
                            objMov.mov_inputOutput = objTop.top_inputOutput;
                            objMov.mov_concept = objRfu.rfu_concept;
                            objMov.mov_dateCreate = DateTime.UtcNow;
                            objMov.mov_validate = false;
                            bdContext.mov_movement.Add(objMov);

                            bdContext.SaveChanges();
                        }
                        else
                        {
                            response.code = 303;
                            response.message = String.Format(Resource.Messages.Error.errorRetireNotApproved, ammount, objBal.gain);
                        }
                    }
                    else
                    {
                        response.code = 204;
                        response.message = Resource.Messages.Warning.errorAmountMaxRetry;
                    }
                }
                else
                {
                    response.code = 203;
                    response.message = Resource.Messages.Warning.errorAccountInvalid;
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
        /// <param name="proId"></param>
        /// <returns></returns>
        public ResponseProjectBankAccount getProjectBankAccount(long proId)
        {
            ResponseProjectBankAccount response = new ResponseProjectBankAccount();

            try
            {
                response = (from objPba in bdContext.SP_GET_PROJECTBANKACCOUNT(proId)
                            select new ResponseProjectBankAccount()
                            {
                                bank = objPba.pba_bank,
                                owner = objPba.pba_owner,
                                accountNbr = objPba.pba_accountNbr,
                                cbu = objPba.pba_cbu,
                                label = objPba.pba_label
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
    }
}
