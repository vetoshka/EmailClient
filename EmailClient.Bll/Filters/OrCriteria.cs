using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace EmailClient.Bll.Filters
{
    public class OrCriteria : ICriteria
    {
        private readonly ICriteria _criteria;
        private readonly ICriteria _otherCriteria;

        public OrCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            this._criteria = criteria;
            this._otherCriteria = otherCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            IEnumerable<MimeMessage> firstCriteriaItems = _criteria.SearchMessages(messages);
            IEnumerable<MimeMessage> otherCriteriaItems = _otherCriteria.SearchMessages(messages);

            return firstCriteriaItems.Union(otherCriteriaItems);
        }
    }
}
