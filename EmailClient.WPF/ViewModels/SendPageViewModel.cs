using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using EmailClient.Annotations;

namespace EmailClient.ViewModels
{
   public class SendPageViewModel : INotifyPropertyChanged
   {
       private readonly EmailMessageBuilder _builder = new EmailMessageBuilder();

       public string Subject { get; set; }
       public string To { get; set; }
       public  string Body { get; set; }
       public string Attachment { get; set; }


       private bool isEnabledSendButton;

       public bool IsEnabledSendButton
       {
           get => isEnabledSendButton;
           set
           {
               isEnabledSendButton = value;
               OnPropertyChanged(nameof(IsEnabledSendButton));
            }
       }


       public event PropertyChangedEventHandler PropertyChanged;

       [NotifyPropertyChangedInvocator]
       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
       }
   }
}
