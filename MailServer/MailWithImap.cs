using System.Collections.Generic;
using EmailClient.Domain.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace EmailClient.MailServer
{
    public class MailWithImap : MailService
    {
        private  ImapClient _imapClient;

        public override bool Login(MailBoxProperties mailBoxProperties)
        {
            _imapClient.AuthenticationMechanisms.Remove("XOAUTH2");
            _imapClient.Authenticate(mailBoxProperties.UserName, mailBoxProperties.Password);
            return _imapClient.IsAuthenticated;
        }

        public override void Connect( MailBoxProperties mailBoxProperties)
            {
                _imapClient = new ImapClient();
                _imapClient.Connect(mailBoxProperties.IncomingServer, mailBoxProperties.IncomingServerPort, true);
                _imapClient.AuthenticationMechanisms.Remove("XOAUTH2");
            }

        public override MailBoxProperties SetMailBoxProperties(string username, string password, string provider)
        {
            return new MailBoxProperties()
            {
                IncomingServer = $"imap.{provider}",
                IncomingServerPort = 993,
                Smtp = $"smtp.{provider}",
                SmtpPort = 465,
                UserName = username,
                Password = password
            };
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
