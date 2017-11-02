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
    public class StudentPageViewModel: INotifyPropertyChanged, IDataErrorInfo
    {
        StudentBLL studentBLL;
        ClassBLL classBLL;
        LanguageLevelBLL languageLevelBLL;
        LanguageBLL languageBLL;


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
        
        public ICommand AddStudentCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        ICollectionView _Students;
        public ICollectionView Students
        {
            get
            {
                return _Students;
            }
        }

        private bool CanAddStudent(object o)
        {
            return string.IsNullOrEmpty(Error);
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
                Error = error;
                return error;
            }
        }

        public StudentPageViewModel(StudentBLL _studentBLL, ClassBLL _classBLL, LanguageLevelBLL _languageLevelBLL, LanguageBLL _languageBLL)
        {
            studentBLL = _studentBLL;
            classBLL = _classBLL;
            languageLevelBLL = _languageLevelBLL;
            languageBLL = _languageBLL;

            var coll = new ObservableCollection<StudentModel>(studentBLL.GetAll().Select(x=>new StudentModel()
            {
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
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveChangesCommand = new RelayCommand(SaveChanges);
            PropertyChanged += this.OnLanguageChanged;
            LoadLanguages();
        }

        void AddStudent(object param)
        {
            studentBLL.Add(FirstName, LastName, Email, PhoneNumber);
            OnPropertyChanged("Students");
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
            StudentModel editedStudent = new StudentModel();
            editedStudent.FirstName = SelectedStudent.FirstName;
            editedStudent.LastName = SelectedStudent.LastName;
            editedStudent.Email = SelectedStudent.Email;
            editedStudent.PhoneNumber = SelectedStudent.PhoneNumber;

            EditedStudent = editedStudent;

            IsOpenEditPopup = true;
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
