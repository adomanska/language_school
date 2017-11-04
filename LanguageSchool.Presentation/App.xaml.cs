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
            ILanguageBLL languageBLL = new LanguageBLL(context);
            ILanguageLevelBLL languageLevelBLL = new LanguageLevelBLL(context);
            IClassBLL classBLL = new ClassBLL(context);
            IStudentBLL studentBLL = new StudentBLL(context);


            StudentPageViewModel sVM = new StudentPageViewModel(studentBLL, classBLL, languageLevelBLL, languageBLL);
            StudentsPage2 studentPage = new StudentsPage2(sVM);

            ClassesPageViewModel cVM = new ClassesPageViewModel(classBLL, languageLevelBLL, languageBLL);
            ClassesPage2 classPage = new ClassesPage2(cVM);
            var mainWindow = new MainWindow(studentPage, classPage);

            Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
