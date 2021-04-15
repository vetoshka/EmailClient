using System.Collections.Generic;
using System.Linq;
using EmailClient.Domain.Models;
using EmailClient.Models;
using MimeKit;

namespace EmailClient.MailServer
{
    public abstract class MailService
    {
        public EmailAccount AddMail(string username, string password, string provider)
        { 
            
            var mailBoxProperties = SetMailBoxProperties( username, password, provider);
            Connect(mailBoxProperties);
            Login(mailBoxProperties);

            return new EmailAccount()
            {
                Emails = FetchAllMessages(),
                MailBoxProperties = mailBoxProperties
            };
        }
        public abstract bool Login(MailBoxProperties mailBoxProperties);
        public  abstract void Connect(MailBoxProperties mailBoxProperties);
        public abstract MailBoxProperties SetMailBoxProperties(string username, string password, string provider);
        public abstract IEnumerable<MimeMessage> FetchAllMessages();


    }
}
