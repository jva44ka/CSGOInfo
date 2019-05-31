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
using System.Windows.Shapes;

namespace WpfLawyersSystem.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangeTeamWindow.xaml
    /// </summary>
    public partial class ChangeTeamWindow : Window
    {
        public ChangeTeamWindow(Models.Team team)
        {
            InitializeComponent();
            ViewModels.OneTeamViewModel vm = new ViewModels.OneTeamViewModel(team);
            this.DataContext = vm;
        }
    }
}
