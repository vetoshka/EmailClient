using EmailClient.Data.Entities;
using EmailClient.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailClient.Bll.MailServer
{
  public class SendService
    {
        public void SendMessages(MimeMessage message , MailBoxPropertiesDto mailBoxProperties)
        {
            using var smtpClient = new SmtpClient();
            smtpClient.Connect(mailBoxProperties.Smtp, mailBoxProperties.SmtpPort, true);
            smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
            smtpClient.Authenticate(mailBoxProperties.UserName, mailBoxProperties.HashedPassword);
            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}
