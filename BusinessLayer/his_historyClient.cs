//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class his_historyClient
    {
        public long his_id { get; set; }
        public long cli_id { get; set; }
        public string cli_firstname { get; set; }
        public string cli_lastname { get; set; }
        public string cli_email { get; set; }
        public string cli_codePhone { get; set; }
        public string cli_phone { get; set; }
        public Nullable<System.DateTime> cli_bornDate { get; set; }
        public Nullable<long> cou_idNationality { get; set; }
        public Nullable<long> cou_idLive { get; set; }
        public string cli_address { get; set; }
        public string cli_cp { get; set; }
        public string cli_city { get; set; }
        public string cli_dni { get; set; }
        public string cli_cuit { get; set; }
        public string cli_cuil { get; set; }
        public Nullable<long> blo_idFront { get; set; }
        public Nullable<long> blo_idBack { get; set; }
        public string cli_password { get; set; }
        public System.DateTime cli_dateCreate { get; set; }
        public Nullable<System.DateTime> cli_dateModify { get; set; }
        public bool cli_enable { get; set; }
        public bool cli_active { get; set; }
        public bool cli_validate { get; set; }
        public Nullable<int> cli_gender { get; set; }
        public Nullable<long> lan_idContact { get; set; }
        public string cli_profession { get; set; }
        public int cli_civilState { get; set; }
        public int cli_typeInvester { get; set; }
        public string cli_idFacebook { get; set; }
        public string cli_hash { get; set; }
        public Nullable<int> cli_exposedPolitician { get; set; }
        public string cli_codeReference { get; set; }
        public Nullable<double> cli_annualIncome { get; set; }
        public Nullable<double> cli_maxInvest { get; set; }
        public System.DateTime his_dateCreate { get; set; }
    }
}