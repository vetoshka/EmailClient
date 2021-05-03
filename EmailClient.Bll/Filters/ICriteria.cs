using System.Collections.Generic;
using MimeKit;

namespace EmailClient.Bll.Filters
{
  public interface ICriteria
  {
      IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages);
  }
}
