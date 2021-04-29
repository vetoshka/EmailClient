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
        private ILiteDatabase _liteDb;
        private bool _disposed;
        private AccountRepository _accountRepository;
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

        public IAccountRepository AccountRepository
        {
            get
            {
                _accountRepository ??= new AccountRepository(LiteDatabase);
                return _accountRepository;
            }
        }

        public ILiteDatabase LiteDatabase
        {
            get
            {
                return _liteDb ??= new LiteDatabase(@"D:\EmailClient\Email.db");
            }
        }
    }
}
