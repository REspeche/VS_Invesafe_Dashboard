using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class ResponseReports : ResponseMessage
    {
        public List<ReportItem> items { get; set; }
    }

    public class ReportItem
    {
        public long id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string file { get; set; }
        public string path { get; set; }
    }
}
