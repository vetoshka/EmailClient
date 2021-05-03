using System.Collections.Generic;
using EmailClient.Data.Entities;
using EmailClient.Models;
using MailKit.Net.Pop3;
using MimeKit;

namespace EmailClient.Bll.MailServer
{
   public class MailWithPop3 : MailService
   {
       private  Pop3Client _pop3Client;

        public override bool Login(MailBoxPropertiesDto mailBoxProperties)
        {
            _pop3Client.AuthenticationMechanisms.Remove("XOAUTH2");
            _pop3Client.Authenticate(mailBoxProperties.UserName, mailBoxProperties.HashedPassword);
            return _pop3Client.IsAuthenticated;
        }

        public override void Connect(MailBoxPropertiesDto mailBoxProperties)
        {
            _pop3Client = new Pop3Client();
            _pop3Client.Connect(mailBoxProperties.IncomingServer, mailBoxProperties.IncomingServerPort, true);;
        }

        public override MailBoxPropertiesDto SetMailBoxProperties(string username, string password, string provider)
        {
            return new MailBoxPropertiesDto()
            {
                IncomingServer = $"pop.{provider}",
                IncomingServerPort = 995,
                Smtp = $"smtp.{provider}",
                SmtpPort = 465,
                UserName = username,
                HashedPassword = password
            };
        }

        public override IEnumerable<MimeMessage> FetchAllMessages()
        {
            List<MimeMessage> mimeMessages = new List<MimeMessage>();
            using (_pop3Client)
            {
                for (int i = 0; i < _pop3Client.Count; i++)
                {
                    var message = _pop3Client.GetMessage(i);
                    mimeMessages.Add(message);
                }
                _pop3Client.Disconnect(true);
            }
          

            return mimeMessages;
        }
   }
}