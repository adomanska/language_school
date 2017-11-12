using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LanguageSchool.Presentation
{
    public class MessageRoutedEventArgs: RoutedEventArgs
    {
        public string Message { get; }

        public MessageRoutedEventArgs(string exceptionMessage)
        {
            Message = exceptionMessage;
        }
    }
}
