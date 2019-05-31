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

namespace WpfLawyersSystem.Views
{
    /// <summary>
    /// Логика взаимодействия для Top10Page.xaml
    /// </summary>
    public partial class Top10Page : Page
    {
        public Top10Page() // (out ViewModels.Top DataContextParam) в параметры после создание TopViewModel
        {
            InitializeComponent();
            /*ViewModels.TopViewModel vm = new ViewModels.TopViewModel();
            DataContextParam = vm;
            DataContext = vm;*/
        }
    }
}
