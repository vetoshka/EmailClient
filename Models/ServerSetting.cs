using System;
using System.Collections.Generic;
using System.Text;

namespace EmailClient.Models
{
  public  class ServerSetting
    {
        public string MessageStorage { get; set; }
        public string ServerType { get; set; }
        public string ServerName { get; set; }
        public int Port { get; set; }
    }
}
