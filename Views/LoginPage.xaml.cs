using System.Windows;
using System.Windows.Controls;
using EmailClient.Domain.Models;

namespace EmailClient
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public static MailBoxProperties mailbox;
        private readonly EmailService _emailService;
        public LoginPage()
        {
            InitializeComponent();
            _emailService = new EmailService();
            mailbox = new MailBoxProperties()
            {
                Imap = "imap.gmail.com",
                ImapPort = 993,
                Smtp = "smtp.gmail.com",
                SmtpPort = 465
            };
        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {

            mailbox.UserName = username.Text;
            mailbox.Password = password.Password;
            var LoggedIn = _emailService.Login(mailbox);
            if (LoggedIn)
            {
                MainWindow.MainFrame.Content = new HomePage();
            }
            else
            {
                error.Text = "There was a problem logging you in to Google Mail.";
            }
        }
    }
}
