using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MimeKit;

namespace EmailClient.Filters
{
    class CriteriaRecipients: ICriteria
    {
        private readonly string searchCriteria;
        public CriteriaRecipients(string searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            return messages.Where(m => m.To.Mailboxes.Any(s => s.Address.Contains(searchCriteria)));
        }
    }
}
