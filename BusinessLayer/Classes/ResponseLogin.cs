using System;

namespace BusinessLayer.Classes
{
    public class ResponseLogin : ResponseMessage
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string token { get; set; }
        public string codePhone { get; set; }
        public string phone { get; set; }
        public int authenticationMethod { get; set; }

        public bool active { get; set; }

        public string locale { get; set; } //language 2 letter

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

        private string _fullPhone = null;
        public string fullPhone
        {
            get
            {
                return (_fullPhone == null || _fullPhone.Equals("")) ? String.Concat(codePhone + phone) : _fullPhone;
            }
            set
            {
                _fullPhone = value;
            }
        }
    }
}
