using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Data.Entities;
using LiteDB;

namespace EmailClient.Data.Interfaces
{
   public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        ILiteDatabase LiteDatabase { get; }
    }
}
