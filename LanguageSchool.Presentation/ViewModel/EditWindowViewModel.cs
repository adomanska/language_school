using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PropertyChanged;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using LanguageSchool.BusinessLogic;

namespace LanguageSchool.Presentation
{
    public class CloseInformationEventArgs: EventArgs
    {
        public StudentModel ResultStudent { get;}

        public CloseInformationEventArgs(StudentModel studentModel)
        {
            ResultStudent = studentModel;
        }
    }

    [AddINotifyPropertyChangedInterface]
    public class EditWindowViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CloseInformationEventArgs> Informed;

        IStudentBLL studentBLL;
        public EditWindowViewModel(IStudentBLL _studentBLL)
        {
            studentBLL = _studentBLL;

            CancelCommand = new RelayCommand(Cancel);
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }
        public StudentModel SelectedStudent;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                if (columnName == nameof(FirstName))
                {
                    Regex firstNameRegex = new Regex(@"[A-Z][a-z]*");
                    if (String.IsNullOrEmpty(FirstName) || !firstNameRegex.IsMatch(FirstName))
                        error = "Invalid First Name";
                }
                if (columnName == nameof(LastName))
                {
                    Regex lastNameRegex = new Regex(@"([A-Z][a-z]*)(-[A-Z][a-z]*)*");
                    if (String.IsNullOrEmpty(LastName) || !lastNameRegex.IsMatch(LastName))
                        error = "Invalid Last Name";
                }
                if (columnName == nameof(Email))
                {
                    Regex emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                    if (String.IsNullOrEmpty(Email) || !emailRegex.IsMatch(Email))
                        error = "Invalid Email Address";
                }
                if (columnName == nameof(PhoneNumber))
                {
                    Regex phoneNumberRegex = new Regex(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$");
                    PhoneNumber = PhoneNumber == null ? "" : PhoneNumber;
                    if (!phoneNumberRegex.IsMatch(PhoneNumber))
                        error = "Invalid Phone Number";
                }

                Error = error;
                return error;
            }
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        void Cancel(object param)
        {
            Informed?.Invoke(this, new CloseInformationEventArgs(null));
        }

        void SaveChanges(object param)
        {
            try
            {
                studentBLL.Update(Email, FirstName, LastName, Email, PhoneNumber);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
