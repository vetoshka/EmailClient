using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Data.Interfaces;
using EmailClient.Data.Repository;
using LiteDB;

namespace EmailClient.Data
{
   public class UnitOfWork : IUnitOfWork
   {
       private readonly EmailDbContext _dbContext;
   
        private AccountRepository _accountRepository;
        private EmailMessageRepository _emailMessageRepository;
        public UnitOfWork()
        {
            _dbContext = new EmailDbContext(new LiteDatabase("EmailDatabase.db"));
        }
 

        public IAccountRepository AccountRepository
        {
            get
            {
                return _accountRepository ??= new AccountRepository(_dbContext);
            }
        }

        public IEmailMessageRepository EmailMessageRepository
        {
            get
            {
                return _emailMessageRepository ??= new EmailMessageRepository(_dbContext);
            }
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
                    _dbContext?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
