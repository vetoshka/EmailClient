using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EmailClient.Domain.Models;
using EmailClient.Log;
using EmailClient.Models;
using EmailClient.Views;

namespace EmailClient
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly IMailService _emailService;
        public HomePage()
        {
            InitializeComponent();
            _emailService = LoginPage.EmailService;
            AccountView.Items.Add(_emailService.MailBoxProperties.UserName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new LoginPage();
        }

        private void DownloadMessages()
        {
            ICollection<EmailMessageModel> messageModels = new List<EmailMessageModel>();
            foreach (var message in _emailService.GetMessages())
            {
                messageModels.Add(new EmailMessageBuilder().CreateFromMimeMessage(message).Build());
            }

            messageList.ItemsSource = messageModels;
        }

        private void New_Message_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new SendPage();
        }

        private void AccountView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DownloadMessages();
        }
    }
}
