using System;
using System.Collections.Generic;
using System.Text;

namespace EmailClient.Exceptions
{
   public class EmailException : Exception
   {
       public EmailException()
       {
    
       }

       public EmailException(string message) : base(message)
       {
       }

       public EmailException(string message, Exception innerException) : base(message, innerException)
       {
       }
    }
}
