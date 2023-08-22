namespace BusinessLayer.Classes
{
    public class ResponseTypeInverter : ResponseMessage
    {
        public int typeInvest { get; set; }
        public int question1 { get; set; }
        public int question2 { get; set; }
        public bool agree { get; set; }
    }
}
