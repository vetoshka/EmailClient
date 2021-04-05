using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Domain.Models;
using MailKit;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;

namespace EmailClient.MailServer
{
   public class MailWithPop3 : IMailService
   {
       private readonly IPop3Client _pop3Client;
       private readonly SmtpClient _smtpClient;

       public MailWithPop3()
       {
           _pop3Client = new Pop3Client();
           _smtpClient = new SmtpClient();
       }
        public  void Connect()
        {
            if (MailBoxProperties == null)
            {
                throw new ArgumentNullException(nameof(MailBoxProperties));
            }
            _pop3Client.Connect(MailBoxProperties.IncomingServer, MailBoxProperties.IncomingServerPort, true);
            _pop3Client.AuthenticationMechanisms.Remove("XOAUTH2");
        }

        public MailBoxProperties MailBoxProperties { get; set; }

        public  IEnumerable<MimeMessage> GetMessages()
        {
            List<MimeMessage> mimeMessages = new List<MimeMessage>();
            using (_pop3Client)
            {
                Connect();
                _pop3Client.Authenticate(MailBoxProperties.UserName, MailBoxProperties.Password);
                for (int i = 0; i < _pop3Client.Count; i++)
                {
                    var message = _pop3Client.GetMessage(i);
                    mimeMessages.Add(message);
                }

                return mimeMessages;
            }
        }

        public  bool Login()
        {

            bool login = false;
            using (_smtpClient)
            {
                _smtpClient.Connect(MailBoxProperties.Smtp, MailBoxProperties.SmtpPort, true);
                _smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                _smtpClient.Authenticate(MailBoxProperties.UserName, MailBoxProperties.Password);
                login = _smtpClient.IsAuthenticated;
            }
            return login;
        }

        public  void SendMessages(MimeMessage message)
        {
            using (_smtpClient)
            {
                Connect();
                _smtpClient.Authenticate(MailBoxProperties.UserName, MailBoxProperties.Password);
                if (!_smtpClient.IsAuthenticated)
                {

                    throw new Exception("Client is not Authenticated");
                }

                _smtpClient.Send(message);

            }
        }
    }
}
