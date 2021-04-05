using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using EmailClient.Domain.Models;
using MimeKit;

namespace EmailClient.Log
{
   public class LoggerMailService : IMailService
   {
       private readonly IMailService _mailService;
       private readonly ILogger _logger = Logger.GetLogger();

       public LoggerMailService(IMailService mailService )
        {
            _mailService = mailService;
        }

       public MailBoxProperties MailBoxProperties
       {
           get => _mailService.MailBoxProperties;
           set => _mailService.MailBoxProperties = value;
       }

       public  IEnumerable<MimeMessage> GetMessages()
        {
           _logger.Information($"Start getting messages from {MailBoxProperties.UserName}");
           IEnumerable<MimeMessage> messages = null;
           try
           {
              messages = _mailService.GetMessages();
              _logger.Information($"All messages have been loaded from {MailBoxProperties.UserName}");
           }
           catch (Exception e)
           {
             _logger.Error($"Error while getting message from {MailBoxProperties.UserName}", e);
           }

           return messages;
        }

        public  bool Login()
        {
            var login = false;
            try
            {
                _logger.Information($"Login to {MailBoxProperties.UserName}");
              login= _mailService.Login();
            }
            catch (Exception e)
            {
                _logger.Error($"Error while logging to {MailBoxProperties.UserName}", e);
            }

            return login;
        }

        public  void Connect()
        {
            try
            {
              _mailService.Connect();
            }
            catch (Exception e)
            {
                _logger.Error($"Error while connecting to {MailBoxProperties.UserName}", e);
            }
        }

        public  void SendMessages(MimeMessage message)
        {
            try
            {
                _mailService.SendMessages(message);
                _logger.Information("The message has been sent");
            }
            catch (Exception e)
            {
                _logger.Error($"Error while sending message to {MailBoxProperties.UserName}", e);
            }
        }
    }
}
