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
    public partial class MainWindow : Window
    {
        static private LanguageSchoolContext context = new LanguageSchoolContext();
        static public StudentBLL studentBLL = new StudentBLL(context);
        static private ClassBLL classBLL = new ClassBLL(context);
        static private LanguageBLL languageBLL = new LanguageBLL(context);
        static private LanguageLevelBLL languageLevelBLL = new LanguageLevelBLL(context);
        static private StudentsPage studentsPage = new StudentsPage(studentBLL);
        static private ClassesPage classesPage = new ClassesPage(classBLL, languageBLL, languageLevelBLL);

        public MainWindow()
        {
            InitializeComponent();
            navigationFrame.Content = new StartPage(studentsPage, classesPage);
        }
    }
}
