namespace BusinessLayer.Classes
{
    public class ResponseSaleInvest : ResponseMessage
    {
        public long id { get; set; }
        public double amount { get; set; }

        public long idSin { get; set; }
        public long amountSin { get; set; }
        public double fractionSin { get; set; }

        public long idPro { get; set; }
        public string namePro { get; set; }
        public double profitability { get; set; }
        public int timeLimit { get; set; }
    }
}
