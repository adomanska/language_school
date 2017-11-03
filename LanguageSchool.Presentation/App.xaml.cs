using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LanguageSchool.BusinessLogic;
using LanguageSchool.Model;

namespace LanguageSchool.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LanguageSchoolContext context = new LanguageSchoolContext();
            LanguageBLL languageBLL = new LanguageBLL(context);
            LanguageLevelBLL languageLevelBLL = new LanguageLevelBLL(context);
            ClassBLL classBLL = new ClassBLL(context);
            IStudentBLL studentBLL = new StudentBLL(context);


            StudentPageViewModel sVM = new StudentPageViewModel(studentBLL, classBLL, languageLevelBLL, languageBLL);
            StudentsPage2 studentPage = new StudentsPage2(sVM);
            
            ClassesPage classPage = new ClassesPage(classBLL, languageBLL, languageLevelBLL);
            var mainWindow = new MainWindow(studentPage, classPage);

            Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
