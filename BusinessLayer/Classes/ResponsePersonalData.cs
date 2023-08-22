using System;

namespace BusinessLayer.Classes
{
    public class ResponsePersonalData : ResponseMessage
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string codePhone { get; set; }
        public string phone { get; set; }
        public double bornDate { get; set; }
        public int exposedPolitician { get; set; }

        public long nationality { get; set; }
        public long live { get; set; }

        public string address { get; set; }
        public string cp { get; set; }
        public string city { get; set; }
        public string cuit { get; set; }
        public long? dniFront { get; set; }
        public long? dniBack { get; set; }

        public bool active { get; set; }
        public bool validate { get; set; }
        public int? gender { get; set; }
        public long? language { get; set; }
        public string profession { get; set; }
        public int civilState { get; set; }
        public int typeInvester { get; set; }

        public string locale { get; set; } //language 2 letter

        public int authenticationMethod { get; set; }

        //Photos
        public string photoDocumentFront { get; set; }
        public string photoDocumentBack { get; set; }

        private string _fullName = null;
        public string fullName
        {
            get
            {
                return (_fullName == null || _fullName.Equals("")) ? String.Concat(firstName + " " + lastName) : _fullName;
            }
            set
            {
                _fullName = value;
            }
        }
    }
}
