using System.Collections.Generic;
using System.Linq;
using EmailClient.Models;
using MimeKit;

namespace EmailClient.MailServer
{
    public abstract class MailService
    {
        public EmailAccountDTO AddMail(string username, string password, string provider)
        { 
            
            var mailBoxProperties = SetMailBoxProperties( username, password, provider);
            Connect(mailBoxProperties);
            Login(mailBoxProperties);

            return new EmailAccountDTO()
            {
                Emails = FetchAllMessages(),
                MailBoxProperties = mailBoxProperties
            };
        }
        public abstract bool Login(MailBoxPropertiesDto mailBoxProperties);
        public  abstract void Connect(MailBoxPropertiesDto mailBoxProperties);
        public abstract MailBoxPropertiesDto SetMailBoxProperties(string username, string password, string provider);
        public abstract IEnumerable<MimeMessage> FetchAllMessages();


    }
}
