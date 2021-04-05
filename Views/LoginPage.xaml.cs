using System.Windows;
using System.Windows.Controls;
using EmailClient.Domain.Models;
using EmailClient.Log;
using EmailClient.MailServer;

namespace EmailClient
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        internal static IMailService EmailService;
        public LoginPage()
        {
            InitializeComponent();
           
        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text.Contains("gmail.com"))
            {
                EmailService = new MailWithImap()
                {
                    MailBoxProperties = SetEmailService("imap", "gmail.com", 993)
                };
            }
            else if( username.Text.Contains("ukr.net"))
            {
                EmailService = new MailWithImap()
                {
                    MailBoxProperties = SetEmailService("imap", "ukr.net", 993)
                };
            }
            else if (username.Text.Contains("i.ua"))
            {
                EmailService = new MailWithPop3()
                {
                    MailBoxProperties = SetEmailService("pop3", "i.ua", 110)
                };
            }
            else
            {
                error.Text = "Provider is not exist";
            }

            EmailService = new LoggerMailService(EmailService);
            
            var LoggedIn = EmailService.Login();
            if (LoggedIn)
            {
                MainWindow.MainFrame.Content = new HomePage();
            }
            else
            {
                error.Text = "There was a problem logging you in to Mail.";
            }
        }

        private MailBoxProperties SetEmailService( string incomingServer, string provider, int incomingPort)
        {
            return new MailBoxProperties()
            {
                IncomingServer = $"{incomingServer}.{provider}",
                IncomingServerPort = incomingPort,
                Smtp = $"smtp.{provider}",
                SmtpPort = 465,
                UserName = username.Text,
                Password = password.Password
            };

      

        }
    }
}
