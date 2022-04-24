using MimeKit;

namespace UserApi.Models
{
    public class Email
    {
        public List<MailboxAddress> Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Email (IEnumerable<string> recipient, string subject, int userId, string token)
        {
            Recipients = new List<MailboxAddress>();
            Recipients.AddRange(recipient.Select(r => new MailboxAddress("", r)));
            Subject = subject;
            Body = $"https://localhost:7208/confirmEmail?UserId={userId}&ConfirmationToken={token}";
        }
    }
}
