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

        public bool DeleteById(int id)
        {
           return _dbContext.EmailMessages.Delete(id);
        }

        public IEnumerable<EmailMessageModel> FindAll()
        {
           return _dbContext.EmailMessages.FindAll();
        }

        public string GetAttachmentNameByIdAndMessageId(int messageId, string attachmentId)
        {
            return _dbContext.EmailMessages.FindById(messageId).AttachmentsNames[attachmentId];
        }

        public IDictionary<string, string> GetAttachmentsByMessageId(int id)
        {
            return _dbContext.EmailMessages.FindById(id).AttachmentsNames;
        }

        public EmailMessageModel GetById(int id)
        {
            return _dbContext.EmailMessages.FindById(id);
        }

        public bool Update(EmailMessageModel entity)
        {
            return _dbContext.EmailMessages.Update(entity);
        }
    }
}
