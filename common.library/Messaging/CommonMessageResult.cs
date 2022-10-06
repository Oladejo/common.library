namespace common.library.Messaging
{
    public class CommonMessageResult
    {
        public CommonMessageResult() => Errormessages = new List<string>();
        public List<string> Errormessages { get; set; }

        public string Status { get; set; }
    }
}
