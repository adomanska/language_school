using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.ComponentModel;
using LanguageSchool.BusinessLogic;
using LanguageSchool.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Data;
using LanguageSchool.Presentation.View;

namespace LanguageSchool.Presentation
{
    [AddINotifyPropertyChangedInterface]
    public class ClassesPageViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        IClassBLL classBLL;
        ILanguageLevelBLL languageLevelBLL;
        ILanguageBLL languageBLL;
        EditClassWindowViewModel eCVM;

        public ICollectionView Classes { get; set; }
        public Class SelectedClass { get; set; }
        public ICollectionView Languages { get; set; }
        public ICollectionView LanguageLevels { get; set; }

        public Language SearchedLanguage { get; set; }
        public LanguageLevel SearchedLevel { get; set; }
        public string SearchedText { get; set; }
        public bool IsLanguageFilterChecked { get; set; }
        public bool IsLevelFilterChecked { get; set; }

        public string NewClassName { get; set; }
        public Language NewLanguage { get; set; }
        public bool IsExistingLanguage { get; set; }
        public string NewLanguageName { get; set; }
        public LanguageLevel NewLanguageLevel { get; set; }
        public int? NewDay { get; set; } 

        public ICommand AddClassCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ClassesPageViewModel(IClassBLL _classBLL, ILanguageLevelBLL _languageLevelBLL, ILanguageBLL _languageBLL)
        {
            classBLL = _classBLL;
            languageLevelBLL = _languageLevelBLL;
            languageBLL = _languageBLL;
            eCVM = new EditClassWindowViewModel(classBLL, languageBLL);
            Classes = CollectionViewSource.GetDefaultView(classBLL.GetAll().Local);
            Classes.SortDescriptions.Add(new System.ComponentModel.SortDescription("ClassName", System.ComponentModel.ListSortDirection.Ascending));

            AddClassCommand = new RelayCommand(AddClass);
            FilterCommand = new RelayCommand(Filter);
            EditCommand = new RelayCommand(Edit);
            Languages = CollectionViewSource.GetDefaultView(languageBLL.GetAll().Local);
            Languages.SortDescriptions.Add(new System.ComponentModel.SortDescription("LanguageName", System.ComponentModel.ListSortDirection.Ascending));
            LanguageLevels = CollectionViewSource.GetDefaultView(languageLevelBLL.GetAll().Local);

            IsExistingLanguage = true;

            eCVM.Languages = Languages;
            eCVM.LanguageLevels = LanguageLevels;
        }

        void AddClass(object param)
        {
            int languageID;
            if (!IsExistingLanguage)
            {
                try
                {
                    languageID = languageBLL.Add(NewLanguageName);
                }
                catch
                {
                    throw;
                }
            }
            else
                languageID = NewLanguage.LanguageID;
            try
            {
                classBLL.Add(NewClassName, DateTime.Now, DateTime.Now.AddHours(2), DayOfWeek.Saturday, languageID , NewLanguageLevel.LanguageLevelID);
            }
            catch
            {
                throw;
            }

            OnPropertyChanged("Classes");
        }
        void Filter(object param)
        {
            Classes.Filter = classBLL.GetFilterPredicate(SearchedText,IsLanguageFilterChecked ? SearchedLanguage : null,IsLevelFilterChecked? SearchedLevel:null);
            OnPropertyChanged("Classes");
        }

        void Edit(object param)
        {
            eCVM.NewClassName = SelectedClass.ClassName;
            eCVM.NewLanguage = SelectedClass.Language;
            eCVM.NewLanguageLevel = SelectedClass.LanguageLevel;
            eCVM.NewDay = 1;
            eCVM.SelectedClass = SelectedClass;

            EditClassWindow editClassWindow = new EditClassWindow(eCVM);
            editClassWindow.ShowDialog();
            Classes.Refresh();
        }
        public string this[string columnName]
        {
            get
            {
                string error = null;

                if(columnName==nameof(NewClassName))
                {
                    if (NewClassName == "" || NewClassName==null)
                        error = "Class Name cannot be blank";
                }
                if(columnName==nameof(NewLanguage) && IsExistingLanguage)
                {
                    if (NewLanguage == null)
                        error = "You have to select language";
                }
                if(columnName==nameof(NewLanguageName) && !IsExistingLanguage)
                {
                    if (languageBLL.Exists(NewLanguageName))
                        error = "Language with such name already exists";
                    if (!languageBLL.IsValidLanguage(NewLanguageName) || NewLanguageName==null)
                        error = "Invalid language name";
                }
                if(columnName==nameof(NewLanguageLevel))
                {
                    if (NewLanguageLevel == null)
                        error = "You have to select language level";
                }
                if (columnName == nameof(NewDay))
                {
                    if (NewDay == null)
                        error = "You have to select day";
                }

                Error = error;
                return error;
            }
        }

        public string Error { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
