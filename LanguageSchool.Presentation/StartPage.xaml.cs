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

namespace LanguageSchool.Presentation
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void goToClasses_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ClassesPage());
        }

        private void goToStudents_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StudentsPage());
        }
    }
}
