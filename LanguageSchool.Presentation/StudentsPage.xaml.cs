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
using System.Data.Entity;

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

            studentBLL.GetAll().Load();
            studentsListBox.ItemsSource = studentBLL.GetAll().Local;

            this.DataContext = this;
        }

        private void goToStartPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(searchBox.Text == "")
                {
                    studentBLL.GetAll().Load();
                    studentsListBox.ItemsSource = studentBLL.GetAll().Local;
                }
                else if ((bool)lastNameRadioButton.IsChecked)
                {
                    StudentsList = new ObservableCollection<Student>(studentBLL.FindByLastName(searchBox.Text));
                    studentsListBox.ItemsSource = CollectionViewSource.GetDefaultView(StudentsList);
                }
                else if ((bool)emailRadioButton.IsChecked)
                {
                    Student student = studentBLL.FindByEmail(searchBox.Text);
                    StudentsList = new ObservableCollection<Student>();
                    StudentsList.Add(student);
                    studentsListBox.ItemsSource = CollectionViewSource.GetDefaultView(StudentsList);
                }
            }
            catch(Exception ex)
            {
                searchErrorTextBlock.Text = ex.Message;
            }
            
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                studentBLL.Add(textBoxFirstName.Text, textBoxLastName.Text, textBoxEmail.Text, textBoxPhone.Text);
                textBoxFirstName.Text = "";
                textBoxLastName.Text = "";
                textBoxEmail.Text = "";
                textBoxPhone.Text = "";
            }
            catch (Exception exception)
            {
                errormessage.Text = exception.Message;
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            editPopup.IsOpen = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            editPopup.IsOpen = false;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            Student oldStudent = (Student)studentsListBox.SelectedItem;
            try
            {
                studentBLL.Update(oldStudent.Email, editTextBoxFirstName.Text, editTextBoxLastName.Text, editTextBoxEmail.Text, editTextBoxPhone.Text);
                editPopup.IsOpen = false;
            }
            catch(Exception exception)
            {
                studentsListBox.SelectedItem = oldStudent;
                editErrormessage.Text = exception.Message;
            }
            
        }
    }
}
