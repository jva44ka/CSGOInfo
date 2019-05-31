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
using WpfLawyersSystem.ViewModels;

namespace WpfLawyersSystem.Views
{
    /// <summary>
    /// Логика взаимодействия для TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {
        public TeamPage(out TeamViewModel DataContextParam)
        {
            InitializeComponent();
            TeamViewModel vm = new ViewModels.TeamViewModel();
            DataContext = vm;
            DataContextParam = vm;
            //DataContext = new ViewModels.TeamViewModel();
        }
    }
}
