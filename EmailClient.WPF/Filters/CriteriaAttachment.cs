using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MimeKit;

namespace EmailClient.Filters
{
    class CriteriaAttachment : ICriteria
    {
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            return messages.Where(m => m.Attachments.Any());
        }
    }
}
