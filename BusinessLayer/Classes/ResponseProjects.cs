using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class ResponseProjects : ResponseMessage
    {
        public List<ProjectItem> items { get; set; }
        public string handlerPath { get; set; }
    }

    public class ProjectItem
    {
        public long id { get; set; }
        public string name { get; set; }
        public string subName { get; set; }
        public string type { get; set; }
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
        public bool finishInvest { get; set; }
    }
}
