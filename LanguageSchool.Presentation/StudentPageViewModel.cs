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

namespace LanguageSchool.Presentation
{
   
    class StudentPageViewModel: INotifyPropertyChanged
    {
        StudentBLL studentBLL;

        public event PropertyChangedEventHandler PropertyChanged;

        string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

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

        bool _isOpenEditPopup;
        public bool IsOpenEditPopup
        {
            get { return _isOpenEditPopup; }
            set
            {
                _isOpenEditPopup = value;
                OnPropertyChanged("IsOpenEditPopup");
            }
        }

        Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                if (_selectedStudent != value)
                {
                    _selectedStudent = value as Student;
                    OnPropertyChanged("SelectedStudent");
                }
            }
        }

        Student _editedStudent;
        public Student EditedStudent
        {
            get { return _editedStudent; }
            set
            {
                if (_editedStudent != value)
                {
                    _editedStudent = value as Student;
                    OnPropertyChanged("EditedStudent");
                }
            }
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

        public StudentPageViewModel(StudentBLL _studentBLL)
        {
            studentBLL = _studentBLL;
            studentBLL.GetAll().Load();
            _Students = CollectionViewSource.GetDefaultView(studentBLL.GetAll().Local);

            IsLastNameFilterChecked = true;
            IsOpenEditPopup = false;

            AddStudentCommand = new RelayCommand(AddStudent);
            FilterCommand = new RelayCommand(Filter);
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveChangesCommand = new RelayCommand(SaveChanges);
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
            Student editedStudent = new Student();
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
