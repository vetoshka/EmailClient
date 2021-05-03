using System.Collections.Generic;
using EmailClient.Filters;
using MimeKit;

namespace EmailClient.Bll.Filters
{
    public class AndCriteria : ICriteria
    {

        private readonly ICriteria _criteria;
        private readonly ICriteria _otherCriteria;

        public AndCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            this._criteria = criteria;
            this._otherCriteria = otherCriteria;
        }
        public IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages)
        {
            IEnumerable<MimeMessage> firstCriteria = _criteria.SearchMessages(messages);
            return _otherCriteria.SearchMessages(firstCriteria);
        }
    }
}
