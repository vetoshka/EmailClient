using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EmailClient.Bll.DTO;
using EmailClient.Bll.Exceptions;
using MimeKit;

namespace EmailClient.Bll
{
   public class EmailMessageBuilder
   {
       private readonly EmailMessageDto _emailMessage;

       public EmailMessageBuilder()
       {
           _emailMessage = new EmailMessageDto() {To = new List<InternetAddress>() , Attachments = new List<string>()};
       }

       public EmailMessageBuilder From(string sender)
       {
           if (!IsEmail(sender)) throw new EmailException("Incorrect email");
            _emailMessage.From = MailboxAddress.Parse(sender);
           return this;
       }
       public EmailMessageBuilder To(string recipient )
       {
           if (!IsEmail(recipient)) throw new EmailException( "Incorrect email");
           _emailMessage.To.Add(MailboxAddress.Parse(recipient));
               return this;
       }
       public EmailMessageBuilder WithBody(string textBody)
       {
           _emailMessage.TextBody = textBody;
           return this;
       }
       public EmailMessageBuilder WithSubject(string subject)
       {
           _emailMessage.Subject = subject;
           return this;
       }

       public bool CanBuild()
       {
           if (_emailMessage.From != null && (_emailMessage.To.Count>0 || !string.IsNullOrEmpty(_emailMessage.Subject)
                                               || !string.IsNullOrEmpty(_emailMessage.TextBody))) return true;
           return false;
       }

   
        public EmailMessageBuilder AddAttachments(string attachments)
       {
           _emailMessage.Attachments.Add(attachments);
           return this;
       }
       public EmailMessageBuilder CreateFromMimeMessage(MimeMessage mailMessage)
       {
           _emailMessage.From = (MailboxAddress)mailMessage.From.First();
           _emailMessage.To = mailMessage.To;
           _emailMessage.Subject = mailMessage.Subject;
           _emailMessage.TextBody = mailMessage.TextBody;
           foreach (var attachment in mailMessage.Attachments)
           {
              
               var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
               _emailMessage.Attachments.Add(fileName);
           }
           return this;
       }

       public EmailMessageDto Build() => _emailMessage;

       public static bool IsEmail(string inputEmail)
       {
           string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                             @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                             @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
           Regex re = new Regex(strRegex);
           if (re.IsMatch(inputEmail.Trim()))
               return true;
           return false;
       }
    }
}
