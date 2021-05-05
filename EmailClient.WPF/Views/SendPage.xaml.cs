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
using EmailClient.Bll;
using EmailClient.Bll.DTO;
using EmailClient.Bll.Exceptions;
using EmailClient.Bll.MailServer;
using EmailClient.Data.Entities;
using EmailClient.Models;
using MailKit;
using MimeKit;

namespace EmailClient.Views
{
    /// <summary>
    /// Interaction logic for SendPage.xaml
    /// </summary>
    public partial class SendPage : Page
    {
        private readonly CreateMessage _emailService;
        private readonly EmailMessageBuilder _builder;

        public SendPage()
        {
            InitializeComponent();
            _builder = new EmailMessageBuilder();
            _emailService = new CreateMessage(new SendService());
          //  _builder.From(emailAccount.MailBoxProperties.UserName);
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
            
           // if(!_builder.CanSend()) error.Text = "message can`t be send";
         //  else
           //{
              // _emailService.SendMessage(_builder.Build(), emailAccount.MailBoxProperties);
               MainWindow.MainFrame.Content = new HomePage();
            //}
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // if (emailAccount.MailBoxProperties.IncomingServer.Contains("imap"))
        //    {
          //      if (_builder.CanBuild()) _emailService.AddDraft(_builder.Build() , emailAccount.MailBoxProperties);
          //  }
            MainWindow.MainFrame.Content = new HomePage();
        }


    }
}
