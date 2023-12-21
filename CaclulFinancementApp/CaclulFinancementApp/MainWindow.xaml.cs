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
using CaclulFinancementApp.Model;
using CaclulFinancementApp.ViewModel;

namespace CaclulFinancementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Système system = new Système();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GestionClientVM(system);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            system.SerializeAll();
        }

    }
}
