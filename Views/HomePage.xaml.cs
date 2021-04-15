using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EmailClient.Domain.Models;
using EmailClient.Log;
using EmailClient.MailServer;
using EmailClient.Models;
using EmailClient.Views;

namespace EmailClient
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public static EmailAccount emailAccount;
        public HomePage()
        {
            InitializeComponent();
            AccountView.Items.Add(emailAccount.MailBoxProperties.UserName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new LoginPage();
        }

        private void DownloadMessages()
        {
            messageList.ItemsSource = emailAccount.Emails;
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
