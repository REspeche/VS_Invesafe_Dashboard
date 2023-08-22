using BusinessLayer.Classes;
using BusinessLayer.Helpers;
using Resource;
using System;
using System.Linq;
using static BusinessLayer.Enum.Tables;

namespace BusinessLayer.DTO
{
    public class PersonalDataDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public ResponsePersonalData GetPersonalData(long cliId, string email)
        {
            ResponsePersonalData response = new ResponsePersonalData();
            try
            {
                response = (from objCli in bdContext.SP_GET_PERSONALDATA(cliId, email)
                                select new ResponsePersonalData()
                            {
                                id = objCli.cli_id,
                                firstName = objCli.cli_firstname,
                                lastName = objCli.cli_lastname,
                                email = objCli.cli_email,
                                codePhone = objCli.cli_codePhone,
                                phone = objCli.cli_phone,
                                bornDate = Commons.DateTimeToUnixTimestamp(objCli.cli_bornDate),
                                address = objCli.cli_address,
                                cp = objCli.cli_cp,
                                city = objCli.cli_city,
                                cuit = objCli.cli_cuit,
                                nationality = objCli.cou_idNationality ?? -1,
                                live = objCli.cou_idLive ?? -1,
                                photoDocumentFront = (objCli.guidFront == null)? null : String.Concat(
                                    CommonHelper.getStoragePath(),
                                    CommonHelper.getContainerName((int)objCli.blo_container),
                                    objCli.guidFront
                                    ),
                                photoDocumentBack = (objCli.guidBack == null) ? null : String.Concat(
                                    CommonHelper.getStoragePath(),
                                    CommonHelper.getContainerName((int)objCli.blo_container),
                                    objCli.guidBack
                                    ),
                                exposedPolitician = objCli.cli_exposedPolitician ?? 0,
                                authenticationMethod = objCli.cli_authenticationMethod
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
        /// <param name="id"></param>
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
        public ResponsePersonalData SavePersonalData(
            long id,
            string firstName,
            string lastName,
            string email,
            string codePhone,
            string phone,
            double bornDate,
            long nationality,
            long live,
            string city,
            string cp,
            string address,
            string cuit,
            int exposedPolitician
            )
        {
            ResponsePersonalData response = new ResponsePersonalData();

            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == id && c.cli_email == email);
                if (objCli != null)
                {
                    //Add client history
                    HistoryDto historyDto = new HistoryDto();
                    historyDto.AddClient(objCli);

                    objCli.cli_firstname = firstName;
                    objCli.cli_lastname = lastName;
                    objCli.cli_codePhone = codePhone;
                    objCli.cli_phone = phone;
                    objCli.cli_bornDate = Commons.UnixTimeStampToDateTime(bornDate);
                    if (nationality > 0) objCli.cou_idNationality = nationality;
                    if (live > 0) objCli.cou_idLive = live;
                    objCli.cli_city = city;
                    objCli.cli_cp = cp;
                    objCli.cli_address = address;
                    objCli.cli_cuit = cuit;
                    objCli.cli_exposedPolitician = exposedPolitician;
                    objCli.cli_codeReference = Commons.RandomReferenceCode(10);
                    objCli.cli_dateModify = DateTime.Now;
                    bdContext.SaveChanges();

                    // Save alerts
                    AlertDto alertDto = new AlertDto();
                    alertDto.ChangeState(id, 2, alertState.Sent);

                    response.firstName = objCli.cli_firstname;
                    response.lastName = objCli.cli_lastname;
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
        /// <param name="email"></param>
        /// <param name="idBlob"></param>
        /// <returns></returns>
        public ResponseMessage UpdateDocumentFront(
            long cliId,
            string email,
            long? idBlob
        )
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == cliId && c.cli_email == email);
                if (objCli != null)
                {
                    objCli.blo_idFront = idBlob;
                    bdContext.SaveChanges();

                    // Save alerts
                    AlertDto alertDto = new AlertDto();
                    alertDto.ChangeState(cliId, 3, alertState.Sent);
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
        /// <param name="email"></param>
        /// <param name="idBlob"></param>
        /// <returns></returns>
        public ResponseMessage UpdateDocumentBack(
            long cliId,
            string email,
            long? idBlob
        )
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == cliId && c.cli_email == email);
                if (objCli != null)
                {
                    objCli.blo_idBack = idBlob;
                    bdContext.SaveChanges();

                    // Save alerts
                    AlertDto alertDto = new AlertDto();
                    alertDto.ChangeState(cliId, 3, alertState.Sent);
                }
                else {
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
        /// <param name="email"></param>
        /// <returns></returns>
        public ResponseAditionalData GetAditionalData(long cliId, string email)
        {
            ResponseAditionalData response = new ResponseAditionalData();
            try
            {
                response = (from objCli in bdContext.SP_GET_ADITIONALDATA(cliId, email)
                            select new ResponseAditionalData()
                            {
                                id = objCli.cli_id,
                                gender = objCli.cli_gender,
                                civilState = objCli.cli_civilState,
                                profession = objCli.cli_profession,
                                annualIncome = objCli.cli_annualIncome ?? 0
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
        /// <param name="gender"></param>
        /// <param name="civilState"></param>
        /// <param name="profession"></param>
        /// <returns></returns>
        public ResponseAditionalData SaveAditionalData(
            long cliId,
            int gender,
            int civilState,
            string profession,
            double annualIncome
            )
        {
            ResponseAditionalData response = new ResponseAditionalData();

            try
            {
                cli_client objCli = bdContext.cli_client.FirstOrDefault((c) => c.cli_id == cliId);
                if (objCli != null)
                {
                    objCli.cli_gender = gender;
                    objCli.cli_civilState = civilState;
                    objCli.cli_profession = profession;
                    objCli.cli_annualIncome = annualIncome;
                    objCli.cli_maxInvest = Math.Round(annualIncome * .2, 2); //20% de los ingresos
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
    }
}
