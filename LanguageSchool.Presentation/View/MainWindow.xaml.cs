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
using LanguageSchool.Model;

namespace LanguageSchool.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            //navigationFrame.Content = new StartPage(studentsPage, classesPage);
        }

        public MainWindow(StudentsPage2 studentPage, ClassesPage classPage)
        {
            InitializeComponent();
            navigationFrame.Content = new StartPage(studentPage, classPage);
        }
    }
}
