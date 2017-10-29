using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LanguageSchool.BusinessLogic;

namespace LanguageSchool.Presentation
{
    /// <summary>
    /// Interaction logic for ClassesPage.xaml
    /// </summary>
    public partial class ClassesPage : Page
    {
        ClassBLL classBLL;
        LanguageBLL languageBLL;
        LanguageLevelBLL languageLevelBLL;
        public ClassesPage(ClassBLL _classBLL, LanguageBLL _languageBLL, LanguageLevelBLL _languageLevelBLL)
        {
            classBLL = _classBLL;
            languageBLL = _languageBLL;
            languageLevelBLL = _languageLevelBLL;
            InitializeComponent();
        }
        private void goToStartPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StartPage.xaml", UriKind.Relative));
        }
    }
}
