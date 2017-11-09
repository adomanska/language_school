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
using LanguageSchool.Model;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace LanguageSchool.Presentation
{
    
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
            SaveCommand = new RelayCommand(Save, CanSave);
        }

        public int ID { get; set; }
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
                    Validator.IsFirstNameValid(FirstName, ref error);
                if (columnName == nameof(LastName))
                    Validator.IsLastNameValid(LastName, ref error);
                if (columnName == nameof(Email))
                    Validator.IsEmailValid(Email, ref error);
                if (PhoneNumber != null && columnName == nameof(PhoneNumber))
                    Validator.IsPhoneNumberValid(PhoneNumber, ref error);

                Error = error;
                return error;
            }
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        void Cancel(object param)
        {
            Informed?.Invoke(this, new CloseInformationEventArgs(null));
        }

        void Save(object param)
        {
            try
            {
                studentBLL.Update(ID, FirstName, LastName, Email, PhoneNumber);
                StudentModel sm = new StudentModel { FirstName = this.FirstName, LastName = this.LastName, Email = this.Email, PhoneNumber = this.PhoneNumber };
                Informed?.Invoke(this, new CloseInformationEventArgs(sm));
            }
            catch (Exception ex)
            {
                ShowMessageDialog(this, new ExceptionMessageRoutedEventArgs(ex.Message));
            }
        }

        private bool CanSave(object o)
        {
            string error = null;
            return Validator.IsFirstNameValid(FirstName, ref error) && Validator.IsLastNameValid(LastName, ref error) &&
                Validator.IsEmailValid(Email, ref error) && Validator.IsPhoneNumberValid(PhoneNumber, ref error);
        }

        private async void ShowMessageDialog(object sender, RoutedEventArgs e)
        {
            ExceptionMessageRoutedEventArgs args = (ExceptionMessageRoutedEventArgs)e;
            await DialogCoordinator.Instance.ShowMessageAsync(this, "Information", args.ExceptionMessage);
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
