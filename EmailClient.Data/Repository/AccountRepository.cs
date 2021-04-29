using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmailClient.Data.Entities;
using EmailClient.Data.Interfaces;
using LiteDB;

namespace EmailClient.Data.Repository
{
   public class AccountRepository : IAccountRepository
   {
       private readonly ILiteCollection<EmailAccount> _accountCollection;
       public AccountRepository(ILiteDatabase liteDatabase)
       {
           _accountCollection = liteDatabase.GetCollection<EmailAccount>("emailAccounts");
       }
        public IEnumerable<EmailAccount> FindAll()
        {
            return _accountCollection.FindAll();
        }

        public EmailAccount GetById(int id)
        {
          return  _accountCollection.FindById(id);
        }


        public EmailAccount GetByUserName(string userName)
        {
            return _accountCollection.FindOne(a => a.MailBoxProperties.UserName == userName);
        }

        public bool DeleteByUserName(string userName)
        {
            var account = GetByUserName(userName);
           return _accountCollection.Delete(account.Id);
        }

        public void Add(EmailAccount entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(EmailAccount));
            _accountCollection.Insert(entity);
        }

        public bool Update(EmailAccount entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(EmailAccount));
           return _accountCollection.Update(entity);

        }

        public bool DeleteById(int id)
        {
            return _accountCollection.Delete(id);
        }

        public void LoadAttachments(string fileName, string directory)
        {
            throw new NotImplementedException();
        }
    }
}
