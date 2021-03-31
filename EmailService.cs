using System;
using System.Collections.Generic;
using EmailClient.Domain.Models;
using EmailClient.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;

namespace EmailClient
{
    public class EmailService
    {
        private readonly ILogger _logger = Logger.GetLogger();
        private void Connect(IMailService client, string host, int port)
        {
            client.Connect(host, port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            if (!client.IsConnected)
            {
                throw new Exception("Error connecting to the client.");
            }
        }


        public bool Login( MailBoxProperties properties)
        {
            using var client = new ImapClient();
            Connect(client, properties.Imap, properties.ImapPort);
            return Authenticate(client, properties.UserName, properties.Password);
        }
        private bool Authenticate(IMailService client, string username, string password)
        {

            try
            {

                client.Authenticate(username, password);

            }
            catch (Exception e)
            {
              _logger.Error("Authenticate Exception", e);
                client.Disconnect(true);
            }

            return client.IsAuthenticated;

        }

        public IEnumerable<MimeMessage>  DownloadMessages(MailBoxProperties properties)
        {
            List<MimeMessage> mimeMessages = new List<MimeMessage>();
            using var imapClient = new ImapClient();

            Connect(imapClient, properties.Imap, properties.ImapPort);
            Authenticate(imapClient, properties.UserName, properties.Password);

            if (!imapClient.IsAuthenticated)
            {

                throw new Exception("Client is not Authenticated");
            }
            imapClient.Inbox.Open(FolderAccess.ReadOnly);
            var ids = imapClient.Inbox.Search(SearchQuery.All);

                foreach (var uid in ids)
                {
                    var message = imapClient.Inbox.GetMessage(uid);
                    mimeMessages.Add(message);
                }
                imapClient.Disconnect(true);
            return mimeMessages;
                
        }

        public void SendMessages(EmailMessageModel messageModel , MailBoxProperties properties)
        {
            using var emailClient = new SmtpClient();


            emailClient.Connect(properties.Smtp, properties.SmtpPort, true);


            emailClient.Authenticate(properties.UserName, properties.Password);

            if (!emailClient.IsAuthenticated)
            {

                throw new Exception("Client is not Authenticated");
            }

            emailClient.Send(CreateMessage(messageModel));

            emailClient.Disconnect(true);
        }


        private MimeMessage CreateMessage(EmailMessageModel messageModel)
        {
            var message = new MimeMessage();
            message.From.Add(messageModel.From);
            message.To.AddRange(messageModel.To);
            message.Subject = messageModel.Subject;
            var builder = new BodyBuilder();
            builder.TextBody = messageModel.TextBody;
            foreach (var attachment in messageModel.Attachments)
            {
                builder.Attachments.Add(attachment);
            }

            message.Body = builder.ToMessageBody();
            return message;

        }

        public void AddDraft(MailBoxProperties properties , EmailMessageModel messageModel)
        {
            using var emailClient = new ImapClient();
            try
            {
                emailClient.Connect(properties.Imap, properties.ImapPort, true);


                emailClient.Authenticate(properties.UserName, properties.Password);

                if (!emailClient.IsAuthenticated)
                {

                    throw new Exception("Client is not Authenticated");
                }


                var draftFolder = emailClient.GetFolder(SpecialFolder.Drafts);
                if (draftFolder == null)
                {
                    var toplevel = emailClient.GetFolder(emailClient.PersonalNamespaces[0]);
                     draftFolder = toplevel.Create(SpecialFolder.Drafts.ToString(), true);
                }
                draftFolder.Open(FolderAccess.ReadWrite);
                draftFolder.Append(CreateMessage(messageModel), MessageFlags.Draft);
                draftFolder.Expunge();
            }
            catch (Exception ex)
            {
                _logger.Error("IMAPException has occured", ex );
            }

            emailClient.Disconnect(true);
        }

    }
}
