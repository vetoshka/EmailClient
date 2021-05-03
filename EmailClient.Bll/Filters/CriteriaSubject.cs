using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmailClient.Bll.Filters;
using MimeKit;

namespace EmailClient.Filters
{
  public  class CriteriaSubject : ICriteria
    {
        private readonly string _searchCriteria;
        public CriteriaSubject(string searchCriteria)
        {
            this._searchCriteria = searchCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            return messages.Where(m => m.Subject.Contains(_searchCriteria));
        }
    }
}
