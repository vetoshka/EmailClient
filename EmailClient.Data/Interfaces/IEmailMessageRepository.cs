using EmailClient.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailClient.Data.Interfaces
{
    public interface IEmailMessageRepository : IRepository<EmailMessageModel>
    {
        public IDictionary<string, string> GetAttachmentsByMessageId(int id);
        public String GetAttachmentNameByIdAndMessageId(int messageId, string attachmentName);  
    }
}
