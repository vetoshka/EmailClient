using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;

namespace EmailClient.Filters
{
  public interface ICriteria
  {
      IEnumerable<MimeMessage> SearchMessages(IEnumerable<MimeMessage> messages);
  }
}
