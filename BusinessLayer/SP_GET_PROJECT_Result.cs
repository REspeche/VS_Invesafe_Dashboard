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
    
    public partial class SP_GET_PROJECT_Result
    {
        public long pro_id { get; set; }
        public string pro_name { get; set; }
        public string pro_subName { get; set; }
        public int pro_typeId { get; set; }
        public string pro_typeName { get; set; }
        public string tpr_icon { get; set; }
        public string pro_status { get; set; }
        public long cou_idNationality { get; set; }
        public string pro_city { get; set; }
        public Nullable<System.DateTime> pro_startFinancing { get; set; }
        public double pro_profitability { get; set; }
        public double pro_historicTIR { get; set; }
        public int pro_timeLimit { get; set; }
        public int pro_goalDays { get; set; }
        public bool pro_finish { get; set; }
        public Nullable<int> countClients { get; set; }
        public Nullable<int> daysRuning { get; set; }
        public double pro_goalAmount { get; set; }
        public Nullable<long> totalInvested { get; set; }
        public Nullable<double> pro_purchasePrice { get; set; }
        public Nullable<double> pro_price1 { get; set; }
        public Nullable<double> pro_price2 { get; set; }
        public Nullable<double> pro_price3 { get; set; }
        public Nullable<double> pro_price4 { get; set; }
        public Nullable<double> pro_priceTotal { get; set; }
        public double minInvest { get; set; }
        public Nullable<double> maxInvest { get; set; }
        public Nullable<double> fractionInitial { get; set; }
        public Nullable<double> fractionActual { get; set; }
        public Nullable<bool> finishInvest { get; set; }
        public Nullable<System.DateTime> endFinancing { get; set; }
    }
}