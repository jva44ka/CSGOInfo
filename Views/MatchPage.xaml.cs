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
using WpfLawyersSystem.ViewModels;

namespace WpfLawyersSystem.Views
{
    /// <summary>
    /// Логика взаимодействия для MatchPage.xaml
    /// </summary>
    public partial class MatchPage : Page
    {
        public MatchPage(out MatchViewModel DataContextParam)
        {
            InitializeComponent();
            ViewModels.MatchViewModel vm = new ViewModels.MatchViewModel();
            DataContext = vm;
            DataContextParam = vm;
        }
    }
}
