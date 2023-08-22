using System;

namespace BusinessLayer.DTO
{
    public class HistoryDto
    {
        private invesafeEntities bdContext = new invesafeEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCli"></param>
        public void AddClient(cli_client objCli)
        {
            try
            {
                his_historyClient objHisCli = new his_historyClient();
                objHisCli.cli_id = objCli.cli_id;
                objHisCli.cli_firstname = objCli.cli_firstname;
                objHisCli.cli_lastname = objCli.cli_lastname;
                objHisCli.cli_email = objCli.cli_email;
                objHisCli.cli_codePhone = objCli.cli_codePhone;
                objHisCli.cli_phone = objCli.cli_phone;
                objHisCli.cli_bornDate = objCli.cli_bornDate;
                objHisCli.cou_idNationality = objCli.cou_idNationality;
                objHisCli.cou_idLive = objCli.cou_idLive;
                objHisCli.cli_address = objCli.cli_address;
                objHisCli.cli_cp = objCli.cli_cp;
                objHisCli.cli_city = objCli.cli_city;
                objHisCli.cli_dni = objCli.cli_dni;
                objHisCli.cli_cuit= objCli.cli_cuit;
                objHisCli.cli_cuil = objCli.cli_cuil;
                objHisCli.blo_idFront = objCli.blo_idFront;
                objHisCli.blo_idBack = objCli.blo_idBack;
                objHisCli.cli_password = objCli.cli_password;
                objHisCli.cli_dateCreate = objCli.cli_dateCreate;
                objHisCli.cli_dateModify = objCli.cli_dateModify;
                objHisCli.cli_enable = objCli.cli_enable;
                objHisCli.cli_active = objCli.cli_active;
                objHisCli.cli_validate = objCli.cli_validate;
                objHisCli.cli_gender = objCli.cli_gender;
                objHisCli.lan_idContact = objCli.lan_idContact;
                objHisCli.cli_profession = objCli.cli_profession;
                objHisCli.cli_civilState = objCli.cli_civilState;
                objHisCli.cli_typeInvester = objCli.cli_typeInvester;
                objHisCli.cli_idFacebook = objCli.cli_idFacebook;
                objHisCli.cli_hash = objCli.cli_hash;
                objHisCli.cli_exposedPolitician = objCli.cli_exposedPolitician;
                objHisCli.cli_codeReference = objCli.cli_codeReference;
                objHisCli.cli_annualIncome = objCli.cli_annualIncome;
                objHisCli.cli_maxInvest = objCli.cli_maxInvest;
                objHisCli.his_dateCreate = DateTime.Now;
                bdContext.his_historyClient.Add(objHisCli);
                bdContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                throw;
            }
        }
    }
}
