using System.Collections.Generic;
using System.Linq;
using EmailClient.Filters;
using MimeKit;

namespace EmailClient.Bll.Filters
{
  public   class CriteriaSender : ICriteria
    {
        private readonly string _searchCriteria;
        public CriteriaSender(string searchCriteria)
        {
            this._searchCriteria = searchCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            return messages.Where(m => m.From.Mailboxes.Any(s =>s.Address.Contains(_searchCriteria)));
        }
    }
}
