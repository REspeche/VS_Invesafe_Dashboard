using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class ResponseDocuments : ResponseMessage
    {
        public List<DocumentItem> items { get; set; }
    }

    public class DocumentItem
    {
        public long cadId { get; set; }
        public string name { get; set; }
        public string file { get; set; }
    }
}
