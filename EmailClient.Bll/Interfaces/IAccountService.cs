using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Bll.DTO;
using EmailClient.Bll.MailServer;
using EmailClient.Models;

namespace EmailClient.Bll.Interfaces
{
   public interface IAccountService
   {
       public IEnumerable<MailBoxPropertiesDto> GetAllAccounts();
       public MailBoxPropertiesDto GetAccountByUserName(string userName);
       public void AddNewAccount(string username, string password, string provider, MailService mailService );
       public bool RemoveAccount(string username);

   }
}
