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
    
    public partial class SP_GET_ALERTS_Result
    {
        public long ale_id { get; set; }
        public string agr_name { get; set; }
        public string ale_message_pending { get; set; }
        public string ale_message_validating { get; set; }
        public string ale_message_approved { get; set; }
        public string ale_message_rejected { get; set; }
        public string ale_link { get; set; }
        public Nullable<int> axc_state { get; set; }
    }
}