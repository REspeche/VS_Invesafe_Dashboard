using System.Collections.Generic;

namespace BusinessLayer.Classes
{
    public class ResponseItemCombo : ResponseMessage
    {
        public List<ComboItem> items { get; set; }
    }

    public class ComboItem
    {
        public long id { get; set; }
        public string label { get; set; }

        public ComboItem()
        {
        }
    }
}
