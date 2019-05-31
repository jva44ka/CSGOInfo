using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfLawyersSystem.Views;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Models.FactoryOfLists.Load();
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Models.FactoryOfLists.Save();
        }
    }
}
