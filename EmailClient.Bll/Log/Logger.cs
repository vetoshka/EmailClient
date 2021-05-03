using System;
using System.IO;

namespace EmailClient.Bll.Log
{
   public class Logger : ILogger
    {
        private const string FilePath = @"D:\Email\EmailClient\EmailClient.Bll\Log\logfile.txt";
        private static readonly Logger _logger = new Logger();
        private Logger()
        {
        }

        public static Logger GetLogger()
        {
            return _logger;
        }
        public void Information(string message)
        {
            WriteToFile($"Information : {message}");
        }

        public void Error(string message, Exception exception)
        {
            WriteToFile($"Error : {message} /n {exception.Message}");
        }


        public void Warning(string message, Exception exception)
        {
            WriteToFile($"Warning : {message} /n {exception.Message}");
        }

        public void Debug(string message)
        {
            WriteToFile($"Debug : {message} ");
        }

        private void WriteToFile( string message)
        {

            using StreamWriter sw = File.AppendText(FilePath);
            sw.WriteLine( $"{DateTime.Now} , {message}");
        }
    }
}
