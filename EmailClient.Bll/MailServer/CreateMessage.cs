using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Models;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace EmailClient.MailServer
{
  public  class CreateMessage
  {
      private readonly SendService _sendService;

      public CreateMessage(SendService sendService)
      {
          _sendService = sendService;
      }




      public void SendMessage(EmailMessageDto messageModel, MailBoxPropertiesDto mailBoxProperties)
      {
            _sendService.SendMessages(NewMimeMessage( messageModel), mailBoxProperties);
      }




        public void AddDraft(EmailMessageDto messagemodel , MailBoxPropertiesDto mailBoxProperties)
        {
           var   message = NewMimeMessage(messagemodel);
            using var imapClient = new ImapClient();
            imapClient.Connect(mailBoxProperties.IncomingServer, mailBoxProperties.IncomingServerPort);
            imapClient.Authenticate(mailBoxProperties.UserName, mailBoxProperties.Password);


            var draftFolder = imapClient.GetFolder(SpecialFolder.Drafts);
            if (draftFolder == null)
            {
                var toplevel = imapClient.GetFolder(imapClient.PersonalNamespaces[0]);
                draftFolder = toplevel.Create(SpecialFolder.Drafts.ToString(), true);
            }
            draftFolder.Open(FolderAccess.ReadWrite);
            draftFolder.Append(message, MessageFlags.Draft);
            draftFolder.Expunge();
        }
        private MimeMessage NewMimeMessage(EmailMessageDto messageModel)
        {
            var message = new MimeMessage();
            message.From.Add(messageModel.From);
            message.To.AddRange(messageModel.To);
            message.Subject = messageModel.Subject;
            var builder = new BodyBuilder
            {
                TextBody = messageModel.TextBody
            };
            foreach (var attachment in messageModel.Attachments)
            {
                builder.Attachments.Add(attachment);
            }

            message.Body = builder.ToMessageBody();
            return message;

        }
    }
}
