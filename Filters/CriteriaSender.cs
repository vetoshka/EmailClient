using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MimeKit;

namespace EmailClient.Filters
{
  public   class CriteriaSender : ICriteria
    {
        private readonly string searchCriteria;
        public CriteriaSender(string searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            return messages.Where(m => m.From.Mailboxes.Any(s =>s.Address.Contains(searchCriteria)));
        }
    }
}
