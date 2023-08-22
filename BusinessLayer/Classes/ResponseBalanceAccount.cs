namespace BusinessLayer.Classes
{
    public class ResponseBalanceAccount : ResponseMessage
    {
        public double balance { get; set; }
        public double gain { get; set; }
        public int authenticationMethod { get; set; }
    }
}
