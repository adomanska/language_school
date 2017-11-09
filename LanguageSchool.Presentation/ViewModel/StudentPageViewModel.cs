using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.ComponentModel;
using LanguageSchool.BusinessLogic;
using LanguageSchool.Model;
using System.Data.Entity;
using System.Windows.Data;
using PropertyChanged;
using System.Text.RegularExpressions;
using MahApps.Metro.Controls.Dialogs;

namespace LanguageSchool.Presentation
{
   [AddINotifyPropertyChangedInterface]
    public class StudentPageViewModel:  INotifyPropertyChanged, IDataErrorInfo
    {
        IStudentBLL studentBLL;
        IClassBLL classBLL;
        ILanguageLevelBLL languageLevelBLL;
        ILanguageBLL languageBLL;

        EditWindowViewModel editWindowVM;
        public IDialogCoordinator dialogCoordinator;

        public event PropertyChangedEventHandler PropertyChanged;

        public StudentPageViewModel(IStudentBLL _studentBLL, IClassBLL _classBLL, ILanguageLevelBLL _languageLevelBLL, ILanguageBLL _languageBLL)
        {
            studentBLL = _studentBLL;
            classBLL = _classBLL;
            languageLevelBLL = _languageLevelBLL;
            languageBLL = _languageBLL;

            editWindowVM = new EditWindowViewModel(studentBLL);

            IsLastNameFilterChecked = true;

            AddStudentCommand = new RelayCommand(AddStudent, CanAddStudent);
            EditCommand = new RelayCommand(Edit, CanUseSelectedStudent);
            SignForClassCommand = new RelayCommand(SignForClass, CanUseSelectedStudent);
            SearchCommand = new RelayCommand(o => Search(o));

            PropertyChanged += this.OnPropertyChanged;
            LoadLanguages();
            PageNumber = 1;
            Search(null);
        }

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

        public ObservableCollection<StudentModel> Students { get; set; }
        public StudentModel SelectedStudent { get; set; }
        public StudentModel EditedStudent { get; set; }

        bool _isAlphabeticallSortSelected;
        public bool IsAlphabeticallSortSelected
        {
            get { return _isAlphabeticallSortSelected; }
            set
            {
                if (_isAlphabeticallSortSelected == value) return;

                _isAlphabeticallSortSelected = value;
                Search(null, PageNumber);
            }
        }

        string _searchedText;
        public string SearchedText
        {
            get { return _searchedText; }
            set
            {
                _searchedText = value;
            }
        }
        public bool IsLastNameFilterChecked { get; set; }
        public bool IsEmailFilterChecked { get; set; }


        public List<string> Languages { get; set; }
        public string SelectedLanguage { get; set; }
        public List<string> LanguageLevels { get; set; }
        public string SelectedLevel { get; set; }
        public List<Class> Classes { get; set; }
        public Class SelectedClass { get; set; }

        private void LoadLanguages()
        {
            Languages = languageBLL.GetAll().Select(x => x.LanguageName).ToList();
            SelectedLanguage = Languages.FirstOrDefault();
        }

        private void LoadLevels()
        {
            LanguageLevels = languageLevelBLL.GetLevels(SelectedLanguage);
            SelectedLevel = LanguageLevels.FirstOrDefault();
        }

        private void LoadClasses()
        {
            Classes = classBLL.GetClasses(SelectedLanguage, SelectedLevel);
            SelectedClass = Classes.FirstOrDefault();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SelectedLanguage))
            {
                LoadLevels();
                LoadClasses();
            }
            if (e.PropertyName == nameof(SelectedLevel))
                LoadClasses();
            if (e.PropertyName == nameof(PageNumber))
                OnStudentListChange();
        }

        public ICommand AddStudentCommand { get; set; }
        void AddStudent(object param)
        {
            try
            {
                studentBLL.Add(FirstName, LastName, Email, PhoneNumber);
                FirstName = null;
                LastName = null;
                Email = null;
                PhoneNumber = null;
            }
            catch (Exception ex)
            {
                ShowMessageDialog(this, new ExceptionMessageRoutedEventArgs(ex.Message));
            }

            OnStudentListChange();
        }

        private bool CanAddStudent(object o)
        {
            string error = null;
            return Validator.IsFirstNameValid(FirstName, ref error) && Validator.IsLastNameValid(LastName, ref error) &&
                Validator.IsEmailValid(Email, ref error) && Validator.IsPhoneNumberValid(PhoneNumber, ref error);
        }

        public ICommand EditCommand { get; set; }
        void Edit(object param)
        {
            editWindowVM.ID = SelectedStudent.ID;
            editWindowVM.FirstName = SelectedStudent.FirstName;
            editWindowVM.LastName = SelectedStudent.LastName;
            editWindowVM.Email = SelectedStudent.Email;
            editWindowVM.PhoneNumber = SelectedStudent.PhoneNumber;

            EditWindow editWindow = new EditWindow(editWindowVM);

            editWindowVM.Informed += (x, e) =>
            {
                editWindow.Close();
                if (e.ResultStudent != null)
                    Search(null, PageNumber);
            };

            editWindow.ShowDialog();
        }

        public ICommand SignForClassCommand { get; set; }
        private void SignForClass(object o)
        {
            try
            {
                studentBLL.SignForClass(SelectedStudent.ID, SelectedClass);
            }
            catch (Exception ex)
            {
                ShowMessageDialog(this, new ExceptionMessageRoutedEventArgs(ex.Message));
            }
        }

        private bool CanUseSelectedStudent(object o)
        {
            return SelectedStudent != null;
        }

        public ICommand SearchCommand { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }

        public void Search(object o, int page = 1)
        {
            var result = studentBLL.Search(new StudentFilter()
            {
                PageNumber = page,
                PageSize = 20,
                Text = SearchedText,
                IsSorted = IsAlphabeticallSortSelected,
                Filter = IsEmailFilterChecked ? DataAccess.SearchBy.Email : DataAccess.SearchBy.LastName
            });

            Students = new ObservableCollection<StudentModel>(result.students.Select(x => new StudentModel()
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }));

            PageCount = result.pageCount;
            PageNumber = page;
        }

        
        private void OnStudentListChange()
        {
            Search(null, PageNumber);
        }

        public void Refresh()
        {
            LoadLanguages();
        }

        private async void ShowMessageDialog(object sender, RoutedEventArgs e)
        {
            ExceptionMessageRoutedEventArgs args = (ExceptionMessageRoutedEventArgs)e;
            await dialogCoordinator.ShowMessageAsync(this, "Information", args.ExceptionMessage);
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
    }
}
