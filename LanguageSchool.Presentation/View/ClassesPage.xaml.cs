using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LanguageSchool.BusinessLogic;
using LanguageSchool.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace LanguageSchool.Presentation
{
    /// <summary>
    /// Interaction logic for ClassesPage.xaml
    /// </summary>
    public partial class ClassesPage : Page
    {
        IClassBLL classBLL;
        ILanguageBLL languageBLL;
        ILanguageLevelBLL languageLevelBLL;
        public ClassesPage(IClassBLL _classBLL, ILanguageBLL _languageBLL, ILanguageLevelBLL _languageLevelBLL)
        {
            classBLL = _classBLL;
            languageBLL = _languageBLL;
            languageLevelBLL = _languageLevelBLL;
            InitializeComponent();
            classesListBox.ItemsSource = classBLL.GetAll().Local;
            classesListBox.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("ClassName", System.ComponentModel.ListSortDirection.Ascending));
            CompositeCollection compositeCollection = new CompositeCollection();
            compositeCollection.Add(new ComboBoxItem { Content = "", Height=30 });
            compositeCollection.Add(new CollectionContainer { Collection = languageBLL.GetAll().Local });
            soughtLanguage.ItemsSource = compositeCollection;
            compositeCollection = new CompositeCollection();
            compositeCollection.Add(new ComboBoxItem { Content = "", Height=30 });
            compositeCollection.Add(new CollectionContainer { Collection = languageLevelBLL.GetAll().Local });
            soughtLevel.ItemsSource = compositeCollection;
            languageComboBox.ItemsSource = languageBLL.GetAll().Local;
            languageLevelComboBox.ItemsSource = languageLevelBLL.GetAll().Local;
        }
        private void goToStartPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            classesListBox.Items.Filter = classBLL.GetFilterPredicate(soughtClassName.Text, soughtLanguage.SelectedItem as Language, soughtLevel.SelectedItem as LanguageLevel);
        }

        private void addClass_Click(object sender, RoutedEventArgs e)
        {
            int languageID;
            if (newLanguageRadioButton.IsChecked.HasValue && newLanguageRadioButton.IsChecked.Value)
            {
                try
                {
                    languageID = languageBLL.Add(newLanguageTextBox.Text);
                }
                catch
                {
                    throw;
                }
            }
            else
                languageID=((Language)languageComboBox.SelectedItem).LanguageID;
            try
            {
                classBLL.Add(classNameTextBox.Text, DateTime.Now, DateTime.Now.AddHours(2), (DayOfWeek)dayOfWeekComboBox.SelectedIndex, languageID, ((LanguageLevel)languageLevelComboBox.SelectedItem).LanguageLevelID);
            }
            catch
            {
                throw;
            }
        }
    }
}
