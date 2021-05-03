using System.Collections.Generic;
using MimeKit;

namespace EmailClient.Bll.DTO
{
   public class EmailMessageDto 
    {
        public int Id { get; set; }
        public MailboxAddress From { get; set; }
        public ICollection<InternetAddress> To { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public ICollection<string> Attachments { get; set; }

    }
}
