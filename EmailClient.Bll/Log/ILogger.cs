using System;

namespace EmailClient.Bll.Log
{
    public interface ILogger
    {
        public void Information(string message);
        public void Error(string message , Exception exception);

        public void Warning(string message, Exception exception);
        public void Debug(string message);


    }
}
