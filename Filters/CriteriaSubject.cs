using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MimeKit;

namespace EmailClient.Filters
{
  public  class CriteriaSubject : ICriteria
    {
        private readonly string searchCriteria;
        public CriteriaSubject(string searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            return messages.Where(m => m.Subject.Contains(searchCriteria));
        }
    }
}
