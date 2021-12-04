

namespace Core.Messaging.Send.Models
{
    public class DispatchEmailSenderModel
    {
        public string Sender { get; set; }
        public string[] Emails { get; set; }
        public string Subject { get; set; }
        public string Contentmessage { get; set; }

    }
}
