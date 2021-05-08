using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EmailClient.Bll.DTO;
using EmailClient.Bll.Interfaces;
using EmailClient.Bll.MailServer;
using EmailClient.Data;
using EmailClient.Data.Entities;
using EmailClient.Data.Interfaces;
using EmailClient.Models;
using MimeKit;

namespace EmailClient.Bll.Services
{
   public class MailBoxService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MailBoxProperties> _mailBoxRepository;
       
        public MailBoxService(IMapper mapper , IRepository<MailBoxProperties> mailBoxRepository)
        {
            _mapper = mapper;
            _mailBoxRepository = mailBoxRepository;

        }
        public IEnumerable<MailBoxPropertiesDto> GetAllAccounts()
        {
            using (_mailBoxRepository)
            {
                return _mapper.Map<IEnumerable<MailBoxProperties>, IEnumerable<MailBoxPropertiesDto>>(_mailBoxRepository.FindAll());
            }
            
        }

        public MailBoxPropertiesDto GetAccountByUserName(string userName)
        {
            using (_mailBoxRepository)
            {
                return _mapper.Map<MailBoxPropertiesDto>(_mailBoxRepository.GetById(userName));
            }
        }

        public void AddNewAccount(string username, string password, string provider , MailService mailService)
        {
            var accountDto = mailService.AddMail(username, password, provider);

            using (_mailBoxRepository)
            {
                var account = _mapper.Map<MailBoxProperties>(accountDto);
               _mailBoxRepository.Add(account);
            }
        }

        public bool RemoveAccount(string username)
        {
            using (_mailBoxRepository)
            {
                return _mailBoxRepository.DeleteById(username);
            }
        }
    }
}
