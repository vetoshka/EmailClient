using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;

namespace EmailClient.Filters
{
    public class AndCriteria : ICriteria
    {

        private readonly ICriteria criteria;
        private readonly ICriteria otherCriteria;

        public AndCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            this.criteria = criteria;
            this.otherCriteria = otherCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            IEnumerable<MimeMessage> firstCriteria = criteria.SearchMessages(messages);
            return otherCriteria.SearchMessages(firstCriteria);
        }
    }
}
