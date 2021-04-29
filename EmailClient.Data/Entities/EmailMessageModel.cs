using System.Collections.Generic;

namespace EmailClient.Data.Entities
{
  public  class EmailMessageModel : BaseEntity
    {
        public string From { get; set; }
        public ICollection<string> To { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public IDictionary<string,string> Attachments { get; set; }
    }
}
