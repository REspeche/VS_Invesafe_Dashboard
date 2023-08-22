using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Classes
{
    public class RequestBankAccount
    {
        public string tacId { get; set; }
        public string holder { get; set; }
        public string accountNbr { get; set; }
        public string bankName { get; set; }
        public string cbu { get; set; }
    }
}
