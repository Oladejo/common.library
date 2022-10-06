namespace common.library.Exception
{
    public class RestException : CustomException
    {
        public RestException(string message) : base(message)
        {

        }

        public RestException(string message, string code) : base(message)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}
