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
    /// Логика взаимодействия для OnePlayerPage.xaml
    /// </summary>
    public partial class CreatePlayerWindow : Window
    {
        public CreatePlayerWindow()
        {
            InitializeComponent();
            ViewModels.OnePlayerViewModel vm = new ViewModels.OnePlayerViewModel();
            this.DataContext = vm;
        }

        public void bClose_Click(object sender, RoutedEventArgs e) { this.Close(); }
    }
}
