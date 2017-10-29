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
    /// Interaction logic for StudentRegistrationView.xaml
    /// </summary>
    public partial class StudentRegistrationView : UserControl
    {
        public StudentRegistrationView()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.studentBLL.Add(textBoxFirstName.Text, textBoxLastName.Text, textBoxEmail.Text, textBoxPhone.Text);
                textBoxFirstName.Text = "";
                textBoxLastName.Text = "";
                textBoxEmail.Text = "";
                textBoxPhone.Text = "";

            }
            catch(Exception exception)
            {
                errormessage.Text = exception.Message;
            }
            
        }
    }
}
