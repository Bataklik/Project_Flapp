using Flapp_BLL.Managers;
using Flapp_PL.View.UserControls;
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
    /// Interaction logic for ZoekTankkaartWindow.xaml
    /// </summary>
    public partial class ZoekTankkaartWindow : Window {
        private MainWindow main;
        private TankkaartUC tUC;

        public ZoekTankkaartWindow(MainWindow main, TankkaartUC tUC) {
            this.main = main;
            this.tUC = tUC;
            InitializeComponent();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            
            if (!string.IsNullOrWhiteSpace(txtKaartnummer.Text) ) {
                main.Show();
                main.wpUserControl.Children.Remove(tUC);
                main.wpUserControl.Children.Add(new TankkaartUC(txtKaartnummer.Text));
                Close();
                return;
            } else if (dpGeldigheidsdatum.SelectedDate != null) {
                main.Show();
                main.wpUserControl.Children.Add(new TankkaartUC((DateTime)dpGeldigheidsdatum.SelectedDate));
                Close();
                return;
            } else if (!string.IsNullOrWhiteSpace(txtKaartnummer.Text) && dpGeldigheidsdatum.SelectedDate != null) {
                main.Show();
                main.wpUserControl.Children.Add(new TankkaartUC(txtKaartnummer.Text , (DateTime)dpGeldigheidsdatum.SelectedDate));
                Close();
                return;
            }
            MessageBox.Show("Velden zijn leeg!");
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
