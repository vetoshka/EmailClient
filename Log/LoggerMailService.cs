using System;
using System.Collections.Generic;
using EmailClient.Domain.Models;
using MimeKit;

namespace EmailClient.Log
{
   public class LoggerMailService : MailServer.MailService
   {
       private readonly MailServer.MailService _mailService;
       private readonly ILogger _logger = Logger.GetLogger();

       public LoggerMailService(MailServer.MailService mailService )
        {
            _mailService = mailService;
        }

        public override bool Login(MailBoxProperties mailBoxProperties)
        {
            var login = false;
            try
            {
                _logger.Information($"Login to {mailBoxProperties.UserName}");
                login = _mailService.Login(mailBoxProperties);
            }
            catch (Exception e)
            {
                _logger.Error($"Error while logging to {mailBoxProperties.UserName}", e);
            }

            return login;
        }

        public override void Connect(MailBoxProperties mailBoxProperties)
        {

            try
            {
                _mailService.Connect(mailBoxProperties);
            }
            catch (Exception e)
            {
                _logger.Error($"Error while connecting to {mailBoxProperties.UserName}", e);
            }
        }

        public override MailBoxProperties SetMailBoxProperties(string username, string password, string provider)
        {
            _logger.Information($"Configuration the {username}");
           return _mailService.SetMailBoxProperties(username, password, provider);
        }

        public override IEnumerable<MimeMessage> FetchAllMessages()
        {
            _logger.Information($"Start getting messages");
            IEnumerable<MimeMessage> messages = null;
            try
            {
                messages = _mailService.FetchAllMessages();
                _logger.Information($"All messages have been loaded");
            }
            catch (Exception e)
            {
                _logger.Error($"Error while getting message ", e);
            }

            return messages;
        }
   }
}
