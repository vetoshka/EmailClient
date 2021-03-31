using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using MailKit;
using MimeKit;

namespace EmailClient.Models
{
   public class EmailMessageModel 
    {

        public int EmailId { get; set; }
        public MailboxAddress From { get; set; }
        public ICollection<InternetAddress> To { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public ICollection<string> Attachments { get; set; }

    }
}
