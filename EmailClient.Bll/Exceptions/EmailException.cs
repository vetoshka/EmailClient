﻿using System;

namespace EmailClient.Bll.Exceptions
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
