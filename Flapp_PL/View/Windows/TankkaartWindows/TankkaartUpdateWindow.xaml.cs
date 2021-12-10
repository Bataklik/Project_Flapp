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

namespace Flapp_PL.View.Windows.TankkaartWindows {
    /// <summary>
    /// Interaction logic for TankkaartUpdateWindow.xaml
    /// </summary>
    public partial class TankkaartUpdateWindow : Window {
        public TankkaartUpdateWindow() {
            InitializeComponent();
        }

        private void btnBestuurder_Click(object sender, RoutedEventArgs e) {
            TankkaartUpdateWindow tuw = this;
            TankkaartZoekBestuurderWindow tzbw = new TankkaartZoekBestuurderWindow(tuw);
            tzbw.ShowDialog();
        }

        private void btnVoegBrandstofToe_Click(object sender, RoutedEventArgs e) {

        }

        private void btnVerwijderBrandstof_Click(object sender, RoutedEventArgs e) {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {

        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e) {

        }
    }
}
