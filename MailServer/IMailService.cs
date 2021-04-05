using System;
using System.Collections.Generic;
using System.Text;
using EmailClient.Domain.Models;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailClient
{
    public interface IMailService
    {
        public MailBoxProperties MailBoxProperties { get; set; }
        public  IEnumerable<MimeMessage> GetMessages();
        public  bool Login();
        public  void Connect();
        public  void SendMessages(MimeMessage message);
    }
}
