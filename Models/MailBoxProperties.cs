using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Models;

namespace EmailClient.Domain.Models
{
  public class MailBoxProperties
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string IncomingServer { get; set; }
        public int IncomingServerPort { get; set; }
        public string Smtp { get; set; }
        public int SmtpPort { get; set; }
        public string Password { get; set; }


    }

}
