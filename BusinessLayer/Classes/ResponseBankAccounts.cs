using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class ResponseBankAccounts : ResponseMessage
    {
        public List<BankAccountItem> items { get; set; }
    }

    public class BankAccountItem
    {
        public long id { get; set; }
        public int tacId { get; set; }
        public string holder { get; set; }
        public string accountNbr { get; set; }
        public string bankName { get; set; }
        public string cbu { get; set; }
        public bool isDefault { get; set; }
        public int state { get; set; }
        public string fileDocument { get; set; }
    }
}
