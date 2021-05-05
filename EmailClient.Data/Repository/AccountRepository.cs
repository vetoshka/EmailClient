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
       private readonly EmailDbContext _dbContext;
       public AccountRepository(EmailDbContext dbContext)
       {
           _dbContext = dbContext;
       }
        public IEnumerable<EmailAccount> FindAll()
        {
            return _dbContext.AccountCollection.Include(x => x.Emails).FindAll();
        }

        public EmailAccount GetById(string id)
        {
          return _dbContext.AccountCollection.FindById(id);
        }


        public EmailAccount GetByUserName(string userName)
        {
            return _dbContext.AccountCollection.FindOne(a => a.MailBoxProperties.UserName == userName);
        }


        public bool DeleteByUserName(string userName)
        {
            var account = GetByUserName(userName);
           return _dbContext.AccountCollection.Delete(account.Id);
        }

        public void Add(EmailAccount entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(EmailAccount));
            _dbContext.AccountCollection.Insert(entity);
        }

        public bool Update(EmailAccount entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(EmailAccount));
           return _dbContext.AccountCollection.Update(entity);

        }

        public bool DeleteById(string id)
        {
            return _dbContext.AccountCollection.Delete(id);
        }

        public void LoadAttachments(string fileName, string directory)
        {
            throw new NotImplementedException();
        }
    }
}
