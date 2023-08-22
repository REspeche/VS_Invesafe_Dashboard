using System;

namespace BusinessLayer.Classes
{
    public class ResponseTwoFactorAuthenticator : ResponseMessage
    {
        public string qrCodeImageUrl { get; set; }
        public string manualEntrySetupCode { get; set; }
    }
}
