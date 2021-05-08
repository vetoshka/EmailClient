using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EmailClient.Bll;
using EmailClient.Bll.DTO;
using EmailClient.Bll.Filters;
using EmailClient.Bll.MailServer;
using EmailClient.Bll.Services;
using EmailClient.Data;
using EmailClient.Data.Repository;
using EmailClient.Filters;
using EmailClient.Models;
using IMapper = AutoMapper.IMapper;
using Mapper = AutoMapper.Mapper;
using MapperConfiguration = AutoMapper.MapperConfiguration;

namespace EmailClient.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {

        public HomePage()
        {
            InitializeComponent();
            var config = new MapperConfiguration(conf => conf.AddProfile(new AutomapperProfile()));
            IMapper mapper = config.CreateMapper();

            var accountService = new MailBoxService(mapper,new AccountRepository());
            accountView.ItemsSource = accountService.GetAllAccounts();

        }

        private void New_Message_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new SendPage();
        }

        private void AccountView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           using var rep = new EmailMessageRepository();
            messageList.ItemsSource= rep.GetAccountMessageByUserName(((MailBoxPropertiesDto) accountView.SelectedItem).UserName);
        }

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new LoginPage();
        }
        private void Filter_OnClick(object sender, RoutedEventArgs e)
        {
            //var criteriaCollection = new List<ICriteria>();
            //var searchText = FilterText.Text;
            
            //if ( RecepientsBtn.IsChecked == true) criteriaCollection.Add( new CriteriaRecipients(searchText));
            //if( SenderBtn.IsChecked == true) criteriaCollection.Add(new CriteriaSender(searchText));
            //ICriteria criteria = null;
            //if (SubjectBtn.IsChecked == true ) criteriaCollection.Add(new CriteriaSubject(searchText));
            //if (criteriaCollection.Count > 0)
            //{
            //     criteria = criteriaCollection[0];

            //    for (int i = 1; i < criteriaCollection.Count; i++)
            //    {
            //        criteria = new OrCriteria(criteria, criteriaCollection[i]);
            //    }

            //    if (AttachmentButton.IsChecked == true) criteria = new AndCriteria(new CriteriaAttachment(), criteria);
            //}
            //else
            //{
            //    if (AttachmentButton.IsChecked == true) criteria = new CriteriaAttachment();
            //}

            //messageList.ItemsSource = criteria != null ? criteria.SearchMessages(((EmailAccountDto)AccountView.SelectedItem).Emails) : ((EmailAccountDto)AccountView.SelectedItem).Emails;
        }

       
    }
}
