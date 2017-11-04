﻿using System;
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
        ClassBLL classBLL;
        LanguageLevelBLL languageLevelBLL;
        LanguageBLL languageBLL;

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
                if (_isAlphabeticallSortSelected)
                {
                    Students.SortDescriptions.Add(new System.ComponentModel.SortDescription("LastName", System.ComponentModel.ListSortDirection.Ascending));
                    Students.SortDescriptions.Add(new System.ComponentModel.SortDescription("FirstName", System.ComponentModel.ListSortDirection.Ascending));
                }
                else Students.SortDescriptions.Clear();

                OnPropertyChanged("IsAlphabeticallSortSelected");
                OnPropertyChanged("Students");
            }
        }

        string _searchedText;
        public string SearchedText
        {
            get { return _searchedText; }
            set
            {
                _searchedText = value;
                if (_searchedText == "")
                    Students.Filter = null;
                else
                    Filter(null);

                OnPropertyChanged("Students");
            }
        }
        public bool IsLastNameFilterChecked { get; set; }
        public bool IsEmailFilterChecked { get; set; }

        public bool IsOpenEditPopup { get; set; }
      
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

        ICollectionView _Students;
        public ICollectionView Students
        {
            get
            {
                return _Students;
            }
        }


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
                if(columnName == nameof(PhoneNumber))
                {
                    Regex phoneNumberRegex = new Regex(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$");
                    PhoneNumber =  PhoneNumber == null ? "" : PhoneNumber;
                    if (!phoneNumberRegex.IsMatch(PhoneNumber))
                        error = "Invalid Phone Number";
                }

                Error = error;
                return error;
            }
        }

        public StudentPageViewModel(IStudentBLL _studentBLL, ClassBLL _classBLL, LanguageLevelBLL _languageLevelBLL, LanguageBLL _languageBLL)
        {
            studentBLL = _studentBLL;
            classBLL = _classBLL;
            languageLevelBLL = _languageLevelBLL;
            languageBLL = _languageBLL;

            editWindowVM = new EditWindowViewModel(studentBLL);

            var coll = new ObservableCollection<StudentModel>(studentBLL.GetAll().Select(x => new StudentModel()
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }));

            _Students = CollectionViewSource.GetDefaultView(coll);

            IsLastNameFilterChecked = true;
            IsOpenEditPopup = false;

            AddStudentCommand = new RelayCommand(AddStudent, CanAddStudent);
            FilterCommand = new RelayCommand(Filter);
            EditCommand = new RelayCommand(Edit, CanUseSelectedStudent);
            CancelCommand = new RelayCommand(Cancel);
            SaveChangesCommand = new RelayCommand(SaveChanges);
            SignForClassCommand = new RelayCommand(SignForClass, CanUseSelectedStudent);

            PropertyChanged += this.OnLanguageChanged;
            LoadLanguages();
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
            
            OnPropertyChanged("Students");
        }

        private bool CanAddStudent(object o)
        {
            return string.IsNullOrEmpty(Error);
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

        void Filter(object param)
        {
            if (IsLastNameFilterChecked)
            {
                Students.Filter = studentBLL.GetFilterByLastNamePredicate(SearchedText);
            }
            else if (IsEmailFilterChecked)
            {
                Students.Filter = studentBLL.GetFilterByEmailPredicate(SearchedText);
            }
            OnPropertyChanged("Students");
        }

        void Edit(object param)
        {
            //StudentModel editedStudent = new StudentModel();
            //editedStudent.FirstName = SelectedStudent.FirstName;
            //editedStudent.LastName = SelectedStudent.LastName;
            //editedStudent.Email = SelectedStudent.Email;
            //editedStudent.PhoneNumber = SelectedStudent.PhoneNumber;

            //EditedStudent = editedStudent;

            editWindowVM.FirstName = SelectedStudent.FirstName;
            editWindowVM.LastName = SelectedStudent.LastName;
            editWindowVM.Email = SelectedStudent.Email;
            editWindowVM.PhoneNumber = SelectedStudent.PhoneNumber;

            EditWindow editWindow = new EditWindow(editWindowVM);
            editWindow.ShowDialog();
            
            //IsOpenEditPopup = true;
        }

        void Cancel(object param)
        {
            IsOpenEditPopup = false;
        }

        void SaveChanges(object param)
        {
            try
            {
                studentBLL.Update(EditedStudent.Email, EditedStudent.FirstName, EditedStudent.LastName, EditedStudent.Email, EditedStudent.PhoneNumber);
                SelectedStudent = EditedStudent;
                OnPropertyChanged("SelectedStudent");
                IsOpenEditPopup = false;
            }
            catch (Exception exception)
            {
                throw;
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