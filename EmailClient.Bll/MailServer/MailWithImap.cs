using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using EmailClient.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using MailService = EmailClient.Bll.MailServer.MailService;

namespace EmailClient.Bll.MailServer
{
    public class MailWithImap : MailService
    {
        private  ImapClient _imapClient;

        public override bool Login(MailBoxPropertiesDto mailBoxProperties)
        {
            _imapClient.AuthenticationMechanisms.Remove("XOAUTH2");
            _imapClient.Authenticate(mailBoxProperties.UserName, mailBoxProperties.HashedPassword);
            return _imapClient.IsAuthenticated;
        }

        public override void Connect( MailBoxPropertiesDto mailBoxProperties)
            {
                _imapClient = new ImapClient();
                _imapClient.Connect(mailBoxProperties.IncomingServer, mailBoxProperties.IncomingServerPort, true);
            }

        public override MailBoxPropertiesDto SetMailBoxProperties(string username, string password, string provider)
        {
           
            return new MailBoxPropertiesDto()
            {
                IncomingServer = $"imap.{provider}",
                IncomingServerPort = 993,
                Smtp = $"smtp.{provider}",
                SmtpPort = 465,
                UserName = username,
                HashedPassword = password
        };
        }

        private byte[] HashPassword(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            var sha1 = new SHA1CryptoServiceProvider();
            return  sha1.ComputeHash(data);
       
        }
        public override IEnumerable<MimeMessage> FetchAllMessages()
        {
            List<MimeMessage> mimeMessages = new List<MimeMessage>();
            using (_imapClient)
            {
                _imapClient.Inbox.Open(FolderAccess.ReadOnly);
                var ids = _imapClient.Inbox.Search(SearchQuery.All);

                foreach (var uid in ids)
                {
                    var message = _imapClient.Inbox.GetMessage(uid);
                    mimeMessages.Add(message);
                }
                _imapClient.Disconnect(true);
                return mimeMessages;
               
            }
        }
    }
}
