
using System.Windows;
using System.Windows.Controls;

namespace EmailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame MainFrame { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MainFrame = mainFrame;
            MainFrame.Content = new LoginPage();
        }
    }
}
