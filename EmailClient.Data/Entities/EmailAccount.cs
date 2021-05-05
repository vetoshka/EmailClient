using System.Collections.Generic;
using LiteDB;

namespace EmailClient.Data.Entities
{
   public class EmailAccount
    {
        [BsonId]
        public int Id { get; set; }
        public MailBoxProperties MailBoxProperties { get; set; }
        public IEnumerable<EmailMessageModel> Emails { get; set; }


    }
}
