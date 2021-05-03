using System.Collections.Generic;
using EmailClient.Models;
using MimeKit;

namespace EmailClient.Bll.DTO
{
   public class EmailAccountDto
    {
        public MailBoxPropertiesDto MailBoxProperties { get; set; }
        public IEnumerable<MimeMessage> Emails { get; set; }
    }
}
