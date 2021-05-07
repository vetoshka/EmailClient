using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmailClient.Data.Entities;
using EmailClient.Data.Interfaces;
using LiteDB;

namespace EmailClient.Data.Repository
{
   public class AccountRepository : IRepository<MailBoxProperties>
   {
       private readonly ILiteCollection<MailBoxProperties> AccountCollection;
       public AccountRepository()
       {
           using var db = new LiteDatabase("EmailDatabase.db");
           AccountCollection = db.GetCollection<MailBoxProperties>("accounts");
       }
        public IEnumerable<MailBoxProperties> FindAll()
        {
            return AccountCollection.FindAll();
        }

        public MailBoxProperties GetById(string userName)
        {
          return AccountCollection.FindById( userName);
        }


        public void Add(MailBoxProperties entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(MailBoxProperties));
            if (GetById(entity.UserName) != null)
                throw new ArgumentException("This account already exist");
            AccountCollection.Insert(entity);
        }

        public bool Update(MailBoxProperties entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(MailBoxProperties));
           return AccountCollection.Update(entity);

        }

        public bool DeleteById(string id)
        {
            return AccountCollection.Delete(id);
        }
   }
}
