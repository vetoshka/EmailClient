using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Domain.Models;

namespace EmailClient.Models
{
   public class EmailAccount
    {
        public MailBoxProperties MailBoxProperties { get; set; }
        public IEnumerable<EmailMessageModel> Emails { get; set; }


    }
}
