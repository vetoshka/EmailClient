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
using MimeKit;

namespace EmailClient.Bll.Services
{
   public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
       
        public AccountService(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        
        }
        public IEnumerable<EmailAccountDto> GetAllAccounts()
        {
            using (_unitOfWork)
            {
                return _mapper.Map<IEnumerable<EmailAccount>, IEnumerable<EmailAccountDto>>(_unitOfWork.AccountRepository.FindAll());
            }

        }

        public EmailAccountDto GetAccountByUserName(string userName)
        {
            using (_unitOfWork)
            {
                return _mapper.Map<EmailAccountDto>(_unitOfWork.AccountRepository.GetByUserName(userName));
            }
     
            
        }

        public void AddNewAccount(string username, string password, string provider , MailService mailService)
        {
           var account = mailService.AddMail(username, password, provider);
           //var emails = _mapper.Map<IEnumerable<EmailMessageModel>>(account.Emails);
           //using (_unitOfWork)
           //{
           //    _unitOfWork.EmailMessageRepository.InsertRange(emails);
           //}

           var accountDto = _mapper.Map<EmailAccount>(account);
          // accountDto.Emails = emails;

            using (_unitOfWork)
            {
                _unitOfWork.AccountRepository.Add(accountDto);
            }
        }

        public bool RemoveAccount(string username)
        {
            using (_unitOfWork)
            {
               return _unitOfWork.AccountRepository.DeleteByUserName(username);
            }
        }
    }
}
