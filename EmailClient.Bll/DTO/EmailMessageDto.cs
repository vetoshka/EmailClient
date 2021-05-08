using System.Collections.Generic;
using MimeKit;

namespace EmailClient.Bll.DTO
{
   public class EmailMessageDto 
    {
        public string Id { get; set; }
        public string AccountUserName { get; set; }
        public string From { get; set; }
        public ICollection<string> To { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public IList<string> AttachmentsNames { get; set; }

    }
}
