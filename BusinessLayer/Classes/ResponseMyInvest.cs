namespace BusinessLayer.Classes
{
    public class ResponseMyInvest : ResponseMessage
    {
        public long id { get; set; }
        public double amount { get; set; }

        public long idSin { get; set; }
        public double amountSin { get; set; }

        public long idPro { get; set; }
        public string namePro { get; set; }
        public double profitability { get; set; }
        public int timeLimit { get; set; }

        public double fractionValue { get; set; }
    }
}
