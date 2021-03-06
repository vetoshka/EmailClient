using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EmailClient.Bll.Log;

namespace EmailClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ILogger logger = Logger.GetLogger();

            logger.Information("Start");
            Window window = new MainWindow();
            window.Show();
            base.OnStartup(e);
        }
    }
}
