using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailClient.MailServer
{
  public class SendService
    {
        public void SendMessages(MimeMessage message , MailBoxProperties mailBoxProperties)
        {
            using var smtpClient = new SmtpClient();
            smtpClient.Connect(mailBoxProperties.Smtp, mailBoxProperties.SmtpPort, true);
            smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
            smtpClient.Authenticate(mailBoxProperties.UserName, mailBoxProperties.Password);
            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}
