using System;

namespace BusinessLayer.Classes
{
    public class CacheParams
    {
        public static string _guidEmpty { get; set; }
        public static string _imageWidthPhoto { get; set; }
        public static string _imageWidthPhotoFull { get; set; }
        public static string _addressInvesafe { get; set; }
        public static string _phoneInvesafe { get; set; }
        public static string _commissionInvestor { get; set; }
        public static string _maxToInvestByTransfer { get; set; }
        public static string _currentUICulture { get; set; }
        public static string _bankAccount { get; set; }
        public static string _emailSupport { get; set; }
        public static string _daysToActivate { get; set; }

        public string guidEmpty
        {
            get { return _guidEmpty; }
            set { _guidEmpty = value; }
        }

        public string imageWidthPhoto
        {
            get { return _imageWidthPhoto; }
            set { _imageWidthPhoto = value; }
        }

        public string imageWidthPhotoFull
        {
            get { return _imageWidthPhotoFull; }
            set { _imageWidthPhotoFull = value; }
        }

        public string addressInvesafe
        {
            get { return _addressInvesafe; }
            set { _addressInvesafe = value; }
        }

        public string phoneInvesafe
        {
            get { return _phoneInvesafe; }
            set { _phoneInvesafe = value; }
        }

        public string commissionInvestor
        {
            get { return _commissionInvestor; }
            set { _commissionInvestor = _commissionInvestor; }
        }

        public string maxToInvestByTransfer
        {
            get { return _maxToInvestByTransfer; }
            set { _maxToInvestByTransfer = value; }
        }

        public string currentUICulture
        {
            get { return _currentUICulture; }
            set { _currentUICulture = value; }
        }

        public string bankAccount
        {
            get { return _bankAccount; }
            set { _bankAccount = value; }
        }

        public string emailSupport
        {
            get { return _emailSupport; }
            set { _emailSupport = value; }
        }

        public string daysToActivate
        {
            get { return _daysToActivate; }
            set { _daysToActivate = value; }
        }
    }
}
