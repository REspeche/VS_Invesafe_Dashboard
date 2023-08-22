using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class ResponseNotifications : ResponseMessage
    {
        public List<NotificationItem> items { get; set; }
    }

    public class NotificationItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool enable { get; set; }
    }
}
