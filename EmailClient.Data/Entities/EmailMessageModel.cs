using System.Collections.Generic;
using LiteDB;

namespace EmailClient.Data.Entities
{
  public  class EmailMessageModel
    {
        [BsonId]
        public string Id { get; set; }
        public string AccountUserName { get; set; }
        public string From { get; set; }
        public ICollection<string> To { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public IList<string> AttachmentsNames { get; set; }
    }
}
