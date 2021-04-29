using System.Collections.Generic;

namespace EmailClient.Data.Entities
{
  public  class EmailAccount : BaseEntity
    {
        public MailBoxProperties MailBoxProperties { get; set; }
        public IEnumerable<EmailMessageModel> Emails { get; set; }
    }
}
