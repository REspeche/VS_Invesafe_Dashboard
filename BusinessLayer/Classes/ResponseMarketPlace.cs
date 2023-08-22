using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Classes
{
    public class ResponseMarketPlace : ResponseMessage
    {
        public List<MarketPlaceItem> items { get; set; }
    }

    public class MarketPlaceItem
    {
        public long id { get; set; }
        public string nameProject { get; set; }
        public string typeProject { get; set; }
        public long amount { get; set; }
        public double fraction { get; set; }
        public double actualFraction { get; set; }
        public double date { get; set; }
        public Boolean canBuy { get; set; }
    }
}
