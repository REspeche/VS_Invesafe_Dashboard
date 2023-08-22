using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class ResponseBankMovements : ResponseMessage
    {
        public List<BankMovementItem> items { get; set; }
    }

    public class BankMovementItem
    {
        public long id { get; set; }
        public string type { get; set; }
        public double amount { get; set; }
        public string inputOutput { get; set; }
        public string concept { get; set; }
        public double dateApply { get; set; }
        public bool validate { get; set; }
    }
}
