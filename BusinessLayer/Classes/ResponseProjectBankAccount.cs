namespace BusinessLayer.Classes
{
    public class ResponseProjectBankAccount : ResponseMessage
    {
        public string bank { get; set; }
        public string owner { get; set; }
        public string accountNbr { get; set; }
        public string cbu { get; set; }
        public string label { get; set; }
        public string referenceCode { get; set; }
    }
}
