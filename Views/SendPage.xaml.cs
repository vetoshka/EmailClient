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

namespace EmailClient.Views
{
    /// <summary>
    /// Interaction logic for SendPage.xaml
    /// </summary>
    public partial class SendPage : Page
    {
        private readonly EmailService _emailService;
        private readonly EmailMessageBuilder _builder;
        private readonly MailBoxProperties mailBox;
        public SendPage()
        {
            InitializeComponent();
            _emailService = new EmailService();
            _builder = new EmailMessageBuilder();
            mailBox = LoginPage.mailbox;
            _builder.From(mailBox.UserName);
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
               _emailService.SendMessages(_builder.Build(), mailBox);
               MainWindow.MainFrame.Content = new HomePage();
            }
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(_builder.CanBuild()) _emailService.AddDraft(mailBox, _builder.Build());
            MainWindow.MainFrame.Content = new HomePage();
        }
    }
}
