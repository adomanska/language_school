﻿using System;
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
    public partial class ClassesPage2 : Page
    {
        public ClassesPage2(ClassesPageViewModel _classesPageViewModel)
        {
            InitializeComponent();
            this.DataContext = _classesPageViewModel;
            Loaded += (x, e) =>
            {
                NavigationService.Navigated += NavigationService_Navigated;
            };
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            var vm = ((ClassesPageViewModel)DataContext);
            vm?.OnPropertyChanged(null);
        }
        private void goToStartPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
