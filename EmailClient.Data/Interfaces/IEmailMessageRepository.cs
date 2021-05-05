using EmailClient.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailClient.Data.Interfaces
{
    public interface IEmailMessageRepository : IRepository<EmailMessageModel>
    {
        public IList<string> GetAttachmentsByMessageId(string id);
        public void InsertRange(IEnumerable<EmailMessageModel> messages);
    }
}
