using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        public ObservableCollection<Student> StudentsList { get; set; }
        StudentBLL studentBLL;
        public StudentsPage(StudentBLL _studentBLL)
        {
            studentBLL = _studentBLL;
            InitializeComponent();
            StudentsList = new ObservableCollection<Student>(MainWindow.studentBLL.GetAll());
            studentsListBox.ItemsSource = CollectionViewSource.GetDefaultView(StudentsList);
            
            this.DataContext = this;
        }

        private void goToStartPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StartPage.xaml", UriKind.Relative));
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)lastNameRadioButton.IsChecked)
                {
                    StudentsList = new ObservableCollection<Student>(studentBLL.FindByLastName(searchBox.Text));
                    studentsListBox.ItemsSource = CollectionViewSource.GetDefaultView(StudentsList);
                }
                else if ((bool)emailRadioButton.IsChecked)
                {
                    StudentsList.Clear();
                    StudentsList.Add(studentBLL.FindByEmail(searchBox.Text));
                    studentsListBox.ItemsSource = CollectionViewSource.GetDefaultView(StudentsList);
                }
            }
            catch(Exception ex)
            {
                searchErrorTextBlock.Text = ex.Message;
            }
            
        }
    }
}
