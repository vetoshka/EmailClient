using System;
using System.Collections.Generic;
using System.Text;

namespace EmailClient
{
    public interface ILogger
    {
        public void Information(string message);
        public void Error(string message , Exception exception);

        public void Warning(string message, Exception exception);
        public void Debug(string message);


    }
}
