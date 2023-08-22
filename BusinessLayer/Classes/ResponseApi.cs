using BusinessLayer.Enum;

namespace BusinessLayer.Classes
{
    public class ResponseApi
    {
        public Api.Result result { get; set; }

        public ResponseApi()
        {
            this.result = Api.Result.Bad;
        }
    }
}
