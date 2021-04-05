using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Domain.Models;
using EmailClient.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;

namespace EmailClient
{
    public class MailWithImap : IMailService
    {
        private readonly ImapClient _imapClient;

        public MailWithImap()
        {
            _imapClient = new ImapClient();
        }
        public  void Connect()
            {
                if (MailBoxProperties == null)
                {
                    throw new ArgumentNullException(nameof(MailBoxProperties));
                }
                _imapClient.Connect(MailBoxProperties.IncomingServer, MailBoxProperties.IncomingServerPort, true);
                _imapClient.AuthenticationMechanisms.Remove("XOAUTH2");
            }

        public MailBoxProperties MailBoxProperties { get; set; }

        public  IEnumerable<MimeMessage> GetMessages()
        {
            List<MimeMessage> mimeMessages = new List<MimeMessage>();
            using (_imapClient)
            {
                Connect();
                _imapClient.Authenticate(MailBoxProperties.UserName, MailBoxProperties.Password);
                _imapClient.Inbox.Open(FolderAccess.ReadOnly);
                var ids = _imapClient.Inbox.Search(SearchQuery.All);

                foreach (var uid in ids)
                {
                    var message = _imapClient.Inbox.GetMessage(uid);
                    mimeMessages.Add(message);
                }
                return mimeMessages;
            }

          
        }

        public  bool Login()
        {

            bool login = false;
            using ( var _smtpClient = new SmtpClient())
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
            using (var _smtpClient = new SmtpClient())
            {
                _smtpClient.Connect(MailBoxProperties.Smtp, MailBoxProperties.SmtpPort, true);
                _smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                _smtpClient.Authenticate(MailBoxProperties.UserName, MailBoxProperties.Password);
                if (!_smtpClient.IsAuthenticated)
                {

                    throw new Exception("Client is not Authenticated");
                }

                _smtpClient.Send(message);

            }
        }

        public void AddDraft(MimeMessage message)
        {
            using (_imapClient)
            {
                Connect();


                _imapClient.Authenticate(MailBoxProperties.UserName, MailBoxProperties.Password);


                var draftFolder = _imapClient.GetFolder(SpecialFolder.Drafts);
                if (draftFolder == null)
                {
                    var toplevel = _imapClient.GetFolder(_imapClient.PersonalNamespaces[0]);
                    draftFolder = toplevel.Create(SpecialFolder.Drafts.ToString(), true);
                }
                draftFolder.Open(FolderAccess.ReadWrite);
                draftFolder.Append(message, MessageFlags.Draft);
                draftFolder.Expunge();
            }

        }
    }
}
