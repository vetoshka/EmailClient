using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using EmailClient.Bll.Log;
using EmailClient.Bll.MailServer;
using EmailClient.Views;

namespace EmailClient
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private  MailService _emailService;
        public LoginPage()
        {
            InitializeComponent();
        }
        private void ContinueBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ProtocolTextBox.Visibility = Visibility.Visible;
            ContinueBtn.Visibility = Visibility.Hidden;
            DoneBtn.Visibility = Visibility.Visible;
            if (email.Text.Contains("gmail.com")) 
            {
                    RadioButton imapRadioButtonb = new RadioButton { IsChecked = false, Content = "Imap" };
                    RadioButton popRadioButtonb = new RadioButton { IsChecked = false, Content = "Pop3" };
                    imapRadioButtonb.Checked += ImapRadioButtonb_Checked;
                    popRadioButtonb.Checked += PopRadioButtonb_Checked;
                    TextBoxes.Children.Add(imapRadioButtonb);
                    TextBoxes.Children.Add(popRadioButtonb);
           

            }
            else if (email.Text.Contains("ukr.net"))
            {
                    _emailService = new LoggerMailService(new MailWithImap());
                TextBoxes.Children.Add(new Label()
                    {
                        Content = "imap.ukr.net"
                    });
            }
            else if (email.Text.Contains("i.ua"))
            {
                    _emailService = new LoggerMailService(new MailWithPop3());
                TextBoxes.Children.Add(new Label()
                    {
                        Content = "pop.i.ua"
                    });

            }
            else
            {
                    error.Text = "Provider is not exist";
                    ProtocolTextBox.Visibility = Visibility.Hidden;
                ContinueBtn.Visibility = Visibility.Visible;
                    DoneBtn.Visibility = Visibility.Hidden;
            }

          


        }
        private void ImapRadioButtonb_Checked(object sender, RoutedEventArgs e)
        {
            _emailService = new LoggerMailService(new MailWithImap());
        }
        private void PopRadioButtonb_Checked(object sender, RoutedEventArgs e)
        {
            _emailService = new LoggerMailService(new MailWithPop3());
        }
        private void Email_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ContinueBtn.IsEnabled = IsValidEmailAddress(email.Text);
            DoneBtn.IsEnabled = IsValidEmailAddress(email.Text);
        }
        private  bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        private void DoneBtn_OnClickBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                HomePage.EmailAccount = _emailService.AddMail(email.Text, password.Password, email.Text.Split('@').Last());
                MainWindow.MainFrame.Content = new HomePage();
            }
            catch (Exception exception)
            {
                Logger.GetLogger().Error("Error while adding the email", exception);
                error.Text = "There was a problem logging you in to Mail.";
            }
        }
    }
}
