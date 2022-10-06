namespace common.library.Messaging
{
    public class CommonEmailRequest
    {
        public string? Body { get; set; }
        public string? Subject { get; set; }
        public string? SenderName { get; set; }
        public string? SenderEmail { get; set; }
        public string? RecipientEmail { get; set; }
        public string? RecipientName { get; set; }
    }
}
