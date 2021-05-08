using EmailClient.Data.Entities;
using EmailClient.Data.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailClient.Data.Repository
{
  public  class EmailMessageRepository : IEmailMessageRepository
    {
        private readonly ILiteCollection<EmailMessageModel> EmailMessages;
        private readonly ILiteDatabase _liteDb;
        public EmailMessageRepository()
        {
            _liteDb = new LiteDatabase(@"Filename=Email.db; Connection=shared");
            EmailMessages = _liteDb.GetCollection<EmailMessageModel>("emails");
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
           return EmailMessages.Find(m => m.AccountUserName == userName);
        }
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _liteDb?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
