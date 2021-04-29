﻿using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Data.Entities;

namespace EmailClient.Data.Interfaces
{
   public interface IAccountRepository: IRepository<EmailAccount>
   { 
       public void LoadAttachments(string fileName , string directory);
       public EmailAccount GetByUserName(string userName);
       public bool DeleteByUserName(string userName);
    }
}