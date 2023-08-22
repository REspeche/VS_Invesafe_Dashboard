namespace BusinessLayer.Classes
{
    public class ResponsePatrimonialSituation : ResponseMessage
    {
        public PatrimonialSituationItem item { get; set; }
    }

    public class PatrimonialSituationItem
    {
        //Resumen Principal
        public double total { get; set; }
        public double available { get; set; }
        public int projects { get; set; }
        public double invest { get; set; }
        public double avgInvest { get; set; }
        public double benefit { get; set; }
        //Beneficios
        public double benefit_1 { get; set; }
        public double benefit_2 { get; set; }
        public double benefit_3 { get; set; }
        public double benefit_4 { get; set; }
        public double benefit_5 { get; set; }
        public double benefitTotal { get; set; }
        //Gastos
        public double expenseInvesafe { get; set; }
        public double expenseExtra { get; set; }
        public double expenseLeak { get; set; }
        public double expenseTotal { get; set; }
        //Chart
        public string chartValues { get; set; }
    }

}
