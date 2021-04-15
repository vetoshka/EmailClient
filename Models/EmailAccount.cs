using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using EmailClient.Domain.Models;
using MimeKit;

namespace EmailClient.Models
{
   public class EmailAccount
    {
        public MailBoxProperties MailBoxProperties { get; set; }
        public IEnumerable<MimeMessage> Emails { get; set; }


    }
}
