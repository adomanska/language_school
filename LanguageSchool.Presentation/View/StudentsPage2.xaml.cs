using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LanguageSchool.BusinessLogic;
using LanguageSchool.Model;
using System.Data.Entity;
using MahApps.Metro.Controls.Dialogs;


namespace LanguageSchool.Presentation
{
    /// <summary>
    /// Interaction logic for StudentsPage2.xaml
    /// </summary>
    public partial class StudentsPage2 : Page
    {
        public StudentsPage2(StudentPageViewModel _studentPageViewModel)
        {
            InitializeComponent();
            _studentPageViewModel.dialogCoordinator = DialogCoordinator.Instance;
            this.DataContext = _studentPageViewModel;

            Loaded += (x, e) =>
            {
                NavigationService.Navigated += NavigationService_Navigated;
            };
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            var vm = ((StudentPageViewModel)DataContext);
            vm?.Refresh();
            vm?.RaisePropertyChanged(null);
        }

        private void goToStartPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
