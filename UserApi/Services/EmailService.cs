using MailKit.Net.Smtp;
using MimeKit;
using UserApi.Models;

namespace UserApi.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string[] recipient, string subject, int userId, string code)
        {
            Email email = new Email(recipient, subject, userId, code);

            var messageEmail = BodyMail(email);

            Send(messageEmail);
        }

        private void Send(MimeMessage messageEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"), true);

                    client.AuthenticationMechanisms.Remove("XOUATH2");

                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password"));

                    client.Send(messageEmail);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage BodyMail(Email message)
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("", _configuration.GetValue<string>("EmailSettings:From")));
            mail.To.AddRange(message.Recipients);
            mail.Subject = message.Subject;
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Body
            };

            return mail;
        }
    }
}
