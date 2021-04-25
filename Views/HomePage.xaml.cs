using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EmailClient.Domain.Models;
using EmailClient.Filters;
using EmailClient.Log;
using EmailClient.MailServer;
using EmailClient.Models;
using EmailClient.Views;
using MimeKit;

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


        private void Filter_OnClick(object sender, RoutedEventArgs e)
        {
            var criteriaCollection = new List<ICriteria>();
            var searchText = FilterText.Text;
            
            if ( RecepientsBtn.IsChecked == true) criteriaCollection.Add( new CriteriaRecipients(searchText));
            if( SenderBtn.IsChecked == true) criteriaCollection.Add(new CriteriaSender(searchText));
            ICriteria criteria = null;
            if (SubjectBtn.IsChecked == true ) criteriaCollection.Add(new CriteriaSubject(searchText));
            if (criteriaCollection.Count > 0)
            {
                 criteria = criteriaCollection[0];

                for (int i = 1; i < criteriaCollection.Count; i++)
                {
                    criteria = new OrCriteria(criteria, criteriaCollection[i]);
                }

                if (AttachmentButton.IsChecked == true) criteria = new AndCriteria(new CriteriaAttachment(), criteria);
            }
            else
            {
                if (AttachmentButton.IsChecked == true) criteria = new CriteriaAttachment();
            }

            messageList.ItemsSource = criteria != null ? criteria.SearchMessages(emailAccount.Emails) : emailAccount.Emails;
        }
    }
}
