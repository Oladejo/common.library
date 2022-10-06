namespace common.library.Messaging
{
    public class CommonEmailMessageRequest
    {
        public Sender? sender { get; set; }
        public Message? message { get; set; }
        public Recipient? recipient { get; set; }
    }


    public class Sender
    {
        public string? fromName { get; set; }
        public string? fromEmailAddress { get; set; }
    }

    public class Message
    {
        public string? templateId { get; set; }
        public string? subject { get; set; }
        public string? body { get; set; }
        public Bodyparams? bodyParams { get; set; }
        public List<string>? attachments { get; set; }
    }

    public class Bodyparams
    {
        public string? msgHeader { get; set; }
        public string? logoUrl { get; set; }
        public string? redirectLink { get; set; }
        public string? redirectLinkTitle { get; set; }
        public string? primaryColour { get; set; }
        public string? secondaryColour { get; set; }
        public string? footerText { get; set; }
        public string? footerAddress { get; set; }
    }

    public class Recipient
    {
        public string? recipientName { get; set; }
        public string? recipientEmail { get; set; }
    }
}
