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
    
    public partial class SP_GET_BANKACCOUNTS_VALIDATED_Result
    {
        public long ban_id { get; set; }
        public Nullable<int> tac_id { get; set; }
        public string ban_bank { get; set; }
        public string ban_accountNumber { get; set; }
        public string ban_cbu { get; set; }
        public string ban_nameTitular { get; set; }
        public bool ban_default { get; set; }
        public Nullable<int> ban_state { get; set; }
        public string guidName { get; set; }
        public Nullable<int> blo_container { get; set; }
    }
}