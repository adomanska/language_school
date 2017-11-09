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

        public event PropertyChangedEventHandler PropertyChanged;

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
       
        public string PhoneNumber { get; set; }
        
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

      
        public StudentModel SelectedStudent { get; set; }
        public StudentModel EditedStudent { get; set; }

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

        private void OnLanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SelectedLanguage))
            {
                LoadLevels();
                LoadClasses();
            }
            if (e.PropertyName == nameof(SelectedLevel))
                LoadClasses();
            if (e.PropertyName == nameof(PageNumber))
                OnPageNumberChange();
        }

        string _exceptionMessage;
        public string ExceptionMessage
        {
            get { return _exceptionMessage; }
            set
            {
                _exceptionMessage = value;
                MessageBox.Show(_exceptionMessage, "Error message");
            }
        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand SignForClassCommand { get; set; }

        public ObservableCollection<StudentModel> Students { get; set; }
        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                if(columnName == nameof(FirstName))
                {
                    Regex firstNameRegex = new Regex(@"[A-Z][a-z]*");
                    if (String.IsNullOrEmpty(FirstName) || !firstNameRegex.IsMatch(FirstName))
                        error = "Invalid First Name";
                }
                if(columnName == nameof(LastName))
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
                if(PhoneNumber != null && columnName == nameof(PhoneNumber))
                {
                    Regex phoneNumberRegex = new Regex(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$");
                    PhoneNumber =  PhoneNumber == null ? "" : PhoneNumber;
                    if (!phoneNumberRegex.IsMatch(PhoneNumber))
                        error = "Invalid Phone Number";
                }
                //if(error != null)
                    Error = error;
                return error;
            }
        }

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
            PropertyChanged += this.OnLanguageChanged;
            LoadLanguages();
            PageNumber = 1;
            Search(null);
        }

        void AddStudent(object param)
        {
            try
            {
                studentBLL.Add(FirstName, LastName, Email, PhoneNumber);
            }
            catch(Exception ex)
            {
                ExceptionMessage = ex.Message;
            }

            OnPageNumberChange();
        }

        private bool CanAddStudent(object o)
        {
            return string.IsNullOrEmpty(Error) ;
        }

        private void SignForClass(object o)
        {
            try
            {
                studentBLL.SignForClass(SelectedStudent.ID, SelectedClass);
            }
            catch(Exception ex)
            {
                ExceptionMessage = ex.Message;
            }
        }

        private bool CanUseSelectedStudent(object o)
        {
            return SelectedStudent != null;
        }


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

        public ICommand SearchCommand { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }

        public void Refresh()
        {
            LoadLanguages();
        }

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

            Students = new ObservableCollection<StudentModel>(result.students.Select(x=>new StudentModel()
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

        private void OnPageNumberChange()
        {
            Search(null, PageNumber);
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
