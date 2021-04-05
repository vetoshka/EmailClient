using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EmailClient.Domain.Models;
using EmailClient.Exceptions;
using EmailClient.Log;
using EmailClient.Models;
using MimeKit;

namespace EmailClient.Views
{
    /// <summary>
    /// Interaction logic for SendPage.xaml
    /// </summary>
    public partial class SendPage : Page
    {
        private readonly IMailService _emailService;
        private readonly EmailMessageBuilder _builder;
        public SendPage()
        {
            InitializeComponent();
            _builder = new EmailMessageBuilder();
            _emailService = LoginPage.EmailService;
         
            _builder.From(_emailService.MailBoxProperties.UserName);
        }

        private void ToChangedEventHandler(object sender, RoutedEventArgs args)
        {
            try
            {
                _builder.To(To.Text);
            }
            catch (EmailException)
            {
                To.Foreground = Brushes.Red;
            }

        }

        private void SubjectChangedEventHandler(object sender, RoutedEventArgs args)
        {
            _builder.WithSubject(Subject.Text);
        }

        private void BodyChangedEventHandler(object sender, RoutedEventArgs args)
        {
            _builder.WithBody(Body.Text);
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            
            if(!_builder.CanSend()) error.Text = "message can`t be send";
           else
           {
               _emailService.SendMessages(CreateMessage(_builder.Build()));
               MainWindow.MainFrame.Content = new HomePage();
            }
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_emailService.MailBoxProperties.IncomingServer.Contains("imap"))
            {
                var mail = _emailService as MailWithImap;
                if (_builder.CanBuild()) mail.AddDraft(CreateMessage(_builder.Build()));
            }
            MainWindow.MainFrame.Content = new HomePage();
        }

        private MimeMessage CreateMessage(EmailMessageModel messageModel)
        {
            var message = new MimeMessage();
            message.From.Add(messageModel.From);
            message.To.AddRange(messageModel.To);
            message.Subject = messageModel.Subject;
            var builder = new BodyBuilder
            {
                TextBody = messageModel.TextBody
            };
            foreach (var attachment in messageModel.Attachments)
            {
                builder.Attachments.Add(attachment);
            }

            message.Body = builder.ToMessageBody();
            return message;

        }
    }
}
