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
        private readonly EmailDbContext _dbContext;
        public EmailMessageRepository(EmailDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(EmailMessageModel entity)
        {
            _dbContext.EmailMessages.Insert(entity);
        }

        public bool DeleteById(string id)
        {
           return _dbContext.EmailMessages.Delete(id);
        }

        public IEnumerable<EmailMessageModel> FindAll()
        {
           return _dbContext.EmailMessages.FindAll();
        }


        public IList<string> GetAttachmentsByMessageId(string id)
        {
            return _dbContext.EmailMessages.FindById(id).AttachmentsNames;
        }

        public EmailMessageModel GetById(string id)
        {
            return _dbContext.EmailMessages.FindById(id);
        }

        public bool Update(EmailMessageModel entity)
        {
            return _dbContext.EmailMessages.Update(entity);
        }


        public void InsertRange(IEnumerable<EmailMessageModel> messages)
        {
            _dbContext.EmailMessages.Insert(messages);
        }
    }
}
