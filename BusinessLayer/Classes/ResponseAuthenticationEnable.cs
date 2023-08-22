namespace BusinessLayer.Classes
{
    public class ResponseAuthenticationEnable : ResponseMessage
    {
        public int authenticationMethod { get; set; }
        public string codePhone { get; set; }
        public string phone { get; set; }
    }
}
