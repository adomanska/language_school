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
        ClassBLL classBLL;
        LanguageBLL languageBLL;
        LanguageLevelBLL languageLevelBLL;
        public ClassesPage(ClassBLL _classBLL, LanguageBLL _languageBLL, LanguageLevelBLL _languageLevelBLL)
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

        //private void addClass_Click(object sender, RoutedEventArgs e)
        //{
        //    string lang = className.Text;
        //    try
        //    {
        //        classBLL.Add(lang, DateTime.Now, DateTime.Now.AddHours(2), DayOfWeek.Monday, ((Language)soughtLanguage.SelectedItem).LanguageID, ((LanguageLevel)soughtLevel.SelectedItem).LanguageLevelID);
        //    }
        //    catch(DbEntityValidationException ex)
        //    {
        //        string exS = "";
        //        foreach (var eve in ex.EntityValidationErrors)
        //        {
        //            exS+=String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:\n",eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //                exS += String.Format("- Property: \"{0}\", Error: \"{1}\"\n", ve.PropertyName, ve.ErrorMessage);
        //        }
        //        MessageBox.Show(exS);
        //    }
        //}

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
