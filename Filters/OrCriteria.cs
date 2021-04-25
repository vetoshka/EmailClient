using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MimeKit;

namespace EmailClient.Filters
{
    public class OrCriteria : ICriteria
    {
        private readonly ICriteria criteria;
        private readonly ICriteria otherCriteria;

        public OrCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            this.criteria = criteria;
            this.otherCriteria = otherCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            IEnumerable<MimeMessage> firstCriteriaItems = criteria.SearchMessages(messages);
            IEnumerable<MimeMessage> otherCriteriaItems = otherCriteria.SearchMessages(messages);

            return firstCriteriaItems.Union(otherCriteriaItems);
        }
    }
}
