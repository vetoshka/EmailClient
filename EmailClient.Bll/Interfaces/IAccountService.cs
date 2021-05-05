using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Bll.DTO;
using EmailClient.Bll.MailServer;

namespace EmailClient.Bll.Interfaces
{
   public interface IAccountService
   {
       public IEnumerable<EmailAccountDto> GetAllAccounts();
       public EmailAccountDto GetAccountByUserName(string userName);
       public void AddNewAccount(string username, string password, string provider, MailService mailService );
       public bool RemoveAccount(string username);

   }
}
