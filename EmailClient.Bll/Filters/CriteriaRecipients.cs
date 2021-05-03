using System.Collections.Generic;
using System.Linq;
using EmailClient.Filters;
using MimeKit;

namespace EmailClient.Bll.Filters
{
    class CriteriaRecipients: ICriteria
    {
        private readonly string _searchCriteria;
        public CriteriaRecipients(string searchCriteria)
        {
            this._searchCriteria = searchCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            return messages.Where(m => m.To.Mailboxes.Any(s => s.Address.Contains(_searchCriteria)));
        }
    }
}
