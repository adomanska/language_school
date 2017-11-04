﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LanguageSchool.BusinessLogic;
using LanguageSchool.Model;
using System.Data.Entity;


namespace LanguageSchool.Presentation
{
    /// <summary>
    /// Interaction logic for StudentsPage2.xaml
    /// </summary>
    public partial class StudentsPage2 : Page
    {
        public ObservableCollection<Student> StudentsList { get; set; }
        public StudentsPage2(StudentPageViewModel vm)
        {
            InitializeComponent();

            this.DataContext = vm;
        }

        private void goToStartPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}