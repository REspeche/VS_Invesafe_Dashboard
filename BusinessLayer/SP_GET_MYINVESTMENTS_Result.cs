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
    
    public partial class SP_GET_MYINVESTMENTS_Result
    {
        public long pxc_id { get; set; }
        public string pro_name { get; set; }
        public string tpr_name { get; set; }
        public long pxc_amount { get; set; }
        public int tpa_id { get; set; }
        public System.DateTime pxc_dateCreate { get; set; }
        public Nullable<long> amountSin { get; set; }
        public Nullable<double> fractionSin { get; set; }
        public bool pro_finish { get; set; }
        public Nullable<double> frv_value { get; set; }
        public Nullable<bool> isSold { get; set; }
    }
}