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
    
    public partial class ban_bankAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ban_bankAccount()
        {
            this.rfu_retireFund = new HashSet<rfu_retireFund>();
        }
    
        public long ban_id { get; set; }
        public Nullable<long> cli_id { get; set; }
        public Nullable<int> tac_id { get; set; }
        public Nullable<long> blo_idDocument { get; set; }
        public string ban_bank { get; set; }
        public string ban_accountNumber { get; set; }
        public string ban_cbu { get; set; }
        public string ban_nameTitular { get; set; }
        public bool ban_default { get; set; }
        public Nullable<int> ban_state { get; set; }
        public System.DateTime ban_dateCreate { get; set; }
        public Nullable<System.DateTime> ban_dateModify { get; set; }
        public bool ban_enable { get; set; }
    
        public virtual tac_typeAccount tac_typeAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rfu_retireFund> rfu_retireFund { get; set; }
        public virtual cli_client cli_client { get; set; }
    }
}
