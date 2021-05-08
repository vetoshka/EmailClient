using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using EmailClient.Bll.DTO;
using EmailClient.Data.Entities;
using EmailClient.Data.Repository;
using EmailClient.Models;
using MimeKit;

namespace EmailClient.Bll.MailServer
{
    public abstract class MailService
    {
        public MailBoxPropertiesDto AddMail(string username, string password, string provider)
        {
            using var repository = new EmailMessageRepository();
            var mailBoxPropertiesDto = SetMailBoxProperties( username, password, provider);
            Connect(mailBoxPropertiesDto);
            Login(mailBoxPropertiesDto);
            var config = new MapperConfiguration(conf => conf.AddProfile(new AutomapperProfile()));
            IMapper mapper = config.CreateMapper();
            var messages = mapper.Map<List<EmailMessageModel>>(FetchAllMessages());
                messages.ForEach(m => m.AccountUserName = mailBoxPropertiesDto.UserName);
            repository.InsertRange(messages);
            return mailBoxPropertiesDto;
        }
        public abstract bool Login(MailBoxPropertiesDto mailBoxProperties);
        public  abstract void Connect(MailBoxPropertiesDto mailBoxProperties);
        public abstract MailBoxPropertiesDto SetMailBoxProperties(string username, string password, string provider);
        public abstract IEnumerable<MimeMessage> FetchAllMessages();


    }
}
