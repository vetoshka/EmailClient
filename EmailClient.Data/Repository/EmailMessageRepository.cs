using EmailClient.Data.Entities;
using EmailClient.Data.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailClient.Data.Repository
{
    class EmailMessageRepository : IEmailMessageRepository
    {
        private readonly ILiteCollection<EmailMessageModel> EmailMessages;
        public EmailMessageRepository()
        {
            using var db = new LiteDatabase("EmailDatabase.db");
            EmailMessages = db.GetCollection<EmailMessageModel>("emails");
        }
        public void Add(EmailMessageModel entity)
        { 
            EmailMessages.Insert(entity);
        }

        public bool DeleteById(string id)
        {
           return EmailMessages.Delete(id);
        }

        public IEnumerable<EmailMessageModel> FindAll()
        {
           return EmailMessages.FindAll();
        }


        public IList<string> GetAttachmentsByMessageId(string id)
        {
            return EmailMessages.FindById(id).AttachmentsNames;
        }

        public EmailMessageModel GetById(string id)
        {
            return EmailMessages.FindById(id);
        }

        public bool Update(EmailMessageModel entity)
        {
            return EmailMessages.Update(entity);
        }


        public void InsertRange(IEnumerable<EmailMessageModel> messages)
        {
            EmailMessages.Insert(messages);
        }

        public void LoadAttachments(string fileName, string directory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmailMessageModel> GetAccountMessageByUserName(string userName)
        {
            EmailMessages.Find(m=>m.AccountId)
        }
    }
}
