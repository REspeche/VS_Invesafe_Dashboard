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
    
    public partial class frv_fractionValue
    {
        public long frv_id { get; set; }
        public long pro_id { get; set; }
        public Nullable<double> frv_uva { get; set; }
        public double frv_value { get; set; }
        public Nullable<System.DateTime> frv_dateReview { get; set; }
        public System.DateTime frv_dateCreate { get; set; }
        public bool frv_enable { get; set; }
    
        public virtual pro_project pro_project { get; set; }
    }
}