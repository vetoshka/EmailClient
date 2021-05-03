using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Data.Entities;
using LiteDB;

namespace EmailClient.Data
{
   public class EmailDbContext :IDisposable
    {
        private readonly ILiteDatabase _liteDb;
        private  ILiteCollection<EmailAccount> _accountCollection;
        private  ILiteCollection<EmailMessageModel> _emailMessages;
        private const string EmailsCollectionName = "emails";
        private const string EmailAccountsCollectionName = "emailAccounts";
        public ILiteCollection<EmailAccount> AccountCollection
        {
            get
            {
                return _accountCollection ??= _liteDb.GetCollection<EmailAccount>(EmailAccountsCollectionName);
            }
        }

        public ILiteCollection<EmailMessageModel> EmailMessages
        {
            get
            {
                return _emailMessages ??= _liteDb.GetCollection<EmailMessageModel>(EmailsCollectionName);
            }
        }
        public EmailDbContext(ILiteDatabase liteDb)
        {
            _liteDb = liteDb;
            SetReferences();
        }

        private void SetReferences()
        {
            _liteDb.Mapper.Entity<EmailAccount>()
                .DbRef(x => x.Emails, EmailsCollectionName);
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
