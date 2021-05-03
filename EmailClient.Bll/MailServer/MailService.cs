using System.Collections.Generic;
using EmailClient.Bll.DTO;
using EmailClient.Models;
using MimeKit;

namespace EmailClient.Bll.MailServer
{
    public abstract class MailService
    {
        public EmailAccountDto AddMail(string username, string password, string provider)
        { 
            
            var mailBoxPropertiesDto = SetMailBoxProperties( username, password, provider);
            Connect(mailBoxPropertiesDto);
            Login(mailBoxPropertiesDto);

            return new EmailAccountDto()
            {
                Emails = FetchAllMessages(),
                MailBoxProperties = mailBoxPropertiesDto
            };
        }
        public abstract bool Login(MailBoxPropertiesDto mailBoxProperties);
        public  abstract void Connect(MailBoxPropertiesDto mailBoxProperties);
        public abstract MailBoxPropertiesDto SetMailBoxProperties(string username, string password, string provider);
        public abstract IEnumerable<MimeMessage> FetchAllMessages();


    }
}
