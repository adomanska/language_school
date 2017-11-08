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

namespace LanguageSchool.Presentation
{
    [AddINotifyPropertyChangedInterface]
    public class EditClassWindowViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CloseInformationEventArgs> Informed;

        IClassBLL classBLL;
        ILanguageBLL languageBLL;
        public ICollectionView Languages { get; set; }
        public ICollectionView LanguageLevels { get; set; }
        public EditClassWindowViewModel(IClassBLL _classBLL, ILanguageBLL _languageBLL)
        {
            classBLL = _classBLL;
            languageBLL = _languageBLL;

            CancelCommand = new RelayCommand(Cancel);
            SaveChangesCommand = new RelayCommand(SaveChanges);
            IsExistingLanguage = true;
        }

        public Class SelectedClass;

        public string NewClassName { get; set; }
        public Language NewLanguage { get; set; }
        public bool IsExistingLanguage { get; set; }
        public string NewLanguageName { get; set; }
        public LanguageLevel NewLanguageLevel { get; set; }
        public int? NewDay { get; set; }
        public int ClassID { get; set; }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = null;

                if (columnName == nameof(NewClassName))
                {
                    if (NewClassName == "" || NewClassName == null)
                        error = "Class Name cannot be blank";
                }
                if (columnName == nameof(NewLanguage) && IsExistingLanguage)
                {
                    if (NewLanguage == null)
                        error = "You have to select language";
                }
                if (columnName == nameof(NewLanguageName) && !IsExistingLanguage)
                {
                    if (languageBLL.Exists(NewLanguageName))
                        error = "Language with such name already exists";
                    if (!languageBLL.IsValidLanguage(NewLanguageName) || NewLanguageName == null)
                        error = "Invalid language name";
                }
                if (columnName == nameof(NewLanguageLevel))
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

        public ICommand CancelCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        void Cancel(object param)
        {
            //Informed?.Invoke(this, new CloseInformationEventArgs(null));
        }

        void SaveChanges(object param)
        {
            int langID = 0;
            if (!IsExistingLanguage)
            {
                try
                {
                    langID = languageBLL.Add(NewLanguageName);
                }
                catch
                {
                    throw;
                }
            }
            else
                langID = NewLanguage.LanguageID;
            try
            {
                classBLL.Update(ClassID, NewClassName, langID, NewLanguageLevel, (DayOfWeek)NewDay);
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
