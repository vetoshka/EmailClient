using System.Collections.Generic;
using System.Linq;
using EmailClient.Filters;
using MimeKit;

namespace EmailClient.Bll.Filters
{
  public  class CriteriaAttachment : ICriteria
    {
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            return messages.Where(m => m.Attachments.Any());
        }
    }
}
