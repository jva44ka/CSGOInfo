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
using WpfLawyersSystem.Models;

namespace WpfLawyersSystem.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangePlayerWindow.xaml
    /// </summary>
    public partial class ChangePlayerWindow : Window
    {
        public ChangePlayerWindow(Player player)
        {
            InitializeComponent();
            ViewModels.OnePlayerViewModel vm = new ViewModels.OnePlayerViewModel(player);
            this.DataContext = vm;
        }

        public void bClose_Click(object sender, EventArgs e) { this.Close(); } // Закрывает окно
    }
}
