using System;

namespace BusinessLayer.Classes
{
    public class ResponseAditionalData : ResponseMessage
    {
        public long id { get; set; }
        public int? gender { get; set; }
        public int civilState { get; set; }
        public string profession { get; set; }
        public double annualIncome { get; set; }
    }
}
