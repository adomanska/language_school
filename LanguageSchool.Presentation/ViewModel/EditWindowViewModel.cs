using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PropertyChanged;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace LanguageSchool.Presentation
{
    [AddINotifyPropertyChangedInterface]
    public class EditWindowViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public StudentModel EditedStudent { get; set; }
        public string FirstName { get; set; }
       
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
                if (columnName == nameof(EditedStudent.LastName))
                {
                    Regex lastNameRegex = new Regex(@"([A-Z][a-z]*)(-[A-Z][a-z]*)*");
                    if (String.IsNullOrEmpty(EditedStudent.LastName) || !lastNameRegex.IsMatch(EditedStudent.LastName))
                        error = "Invalid Last Name";
                }
                if (columnName == nameof(EditedStudent.Email))
                {
                    Regex emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                    if (String.IsNullOrEmpty(EditedStudent.Email) || !emailRegex.IsMatch(EditedStudent.Email))
                        error = "Invalid Email Address";
                }
                if (columnName == nameof(EditedStudent.PhoneNumber))
                {
                    Regex phoneNumberRegex = new Regex(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$");
                    EditedStudent.PhoneNumber = EditedStudent.PhoneNumber == null ? "" : EditedStudent.PhoneNumber;
                    if (!phoneNumberRegex.IsMatch(EditedStudent.PhoneNumber))
                        error = "Invalid Phone Number";
                }

                Error = error;
                return error;
            }
        }

        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
