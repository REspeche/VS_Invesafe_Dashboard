using System;
using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class ResponseAlerts : ResponseMessage
    {
        public List<AlertItem> items { get; set; }
    }

    public class AlertItem
    {
        public long id { get; set; }
        public string groupName { get; set; }
        public string mesage { get; set; }
        public string link { get; set; }
        public int state { get; set; }
    }
}
