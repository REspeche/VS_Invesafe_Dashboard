using System;
using BusinessLayer.Enum;
using Resource;

namespace BusinessLayer.Classes
{
    public class ResponseMessage
    {
        public int code { get; set; }
        public string message { get; set; }
        public Rules.actionMessage action { get; set; }

        public ResponseMessage()
        {
            this.code = 0;
            this.message = Resource.Messages.Success.notifyOK;
            this.action = Rules.actionMessage.None;
        }
    }
}
