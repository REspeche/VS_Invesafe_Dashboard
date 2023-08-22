namespace BusinessLayer.Classes
{
    public class ResponseProject : ResponseMessage
    {
        public long id { get; set; }
        public string name { get; set; }
        public string subName { get; set; }
        public int typeId { get; set; }
        public string typeName { get; set; }
        public string status { get; set; }
        public string city { get; set; }
        public double profitability { get; set; }
        public int timeLimit { get; set; }
        public int goalDays { get; set; }
        public bool finish { get; set; }
        public int countClients { get; set; }
        public double historicTIR { get; set; }
        public int daysRuning { get; set; }
        public double goalAmount { get; set; }
        public double totalInvested { get; set; }
        public double percentInvested { get; set; }
        public string icon { get; set; }
        public double purchasePrice { get; set; }
        public double price1 { get; set; }
        public double price2 { get; set; }
        public double price3 { get; set; }
        public double price4 { get; set; }
        public double priceTotal { get; set; }
        public double startFinancing { get; set; }
        public double endFinancing { get; set; }
        public double minInvest { get; set; }
        public double maxInvest { get; set; }
        public double maxInvestTransfer { get; set; }
        public string handlerPath { get; set; }
        public double fractionInitial { get; set; }
        public double fractionActual { get; set; }
        public bool finishInvest { get; set; }
        public double restAmount { get; set; }
    }
}
