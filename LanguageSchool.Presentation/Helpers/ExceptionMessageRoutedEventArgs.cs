using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LanguageSchool.Presentation
{
    public class ExceptionMessageRoutedEventArgs: RoutedEventArgs
    {
        public string ExceptionMessage { get; }

        public ExceptionMessageRoutedEventArgs(string exceptionMessage)
        {
            ExceptionMessage = exceptionMessage;
        }
    }
}
