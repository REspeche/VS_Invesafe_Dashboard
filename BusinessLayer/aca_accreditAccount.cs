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
    
    public partial class aca_accreditAccount
    {
        public long aca_id { get; set; }
        public Nullable<long> cli_id { get; set; }
        public Nullable<int> tin_typeInvest { get; set; }
        public Nullable<int> aca_question1 { get; set; }
        public Nullable<int> aca_question2 { get; set; }
        public bool aca_agree { get; set; }
        public System.DateTime aca_dateCreate { get; set; }
        public bool aca_enable { get; set; }
    
        public virtual cli_client cli_client { get; set; }
    }
}
