using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class ResponseMyInvestment : ResponseMessage
    {
        public List<InvestmentItem> items { get; set; }
    }

    public class InvestmentItem
    {
        public long id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string payment { get; set; }
        public long amount { get; set; }
        public double fraction { get; set; }
        public double date { get; set; }
        public double amountSin { get; set; }
        public double fractionSin { get; set; }
        public bool finish { get; set; }
        public bool isSold { get; set; }
    }
}
