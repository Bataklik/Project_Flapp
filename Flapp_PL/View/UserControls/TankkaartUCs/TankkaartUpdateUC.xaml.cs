using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Flapp_PL.View.UserControls.TankkaartUCs {
    /// <summary>
    /// Interaction logic for TankkaartUpdateUC.xaml
    /// </summary>
    public partial class TankkaartUpdateUC : UserControl {
        private Tankkaart _tankkaart;
        private BestuurderManager _bestuurderManager;
        private MainWindow _main;

        public TankkaartUpdateUC(Tankkaart t, MainWindow main) {
            InitializeComponent();
            _tankkaart = t;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(ConfigurationManager.ConnectionStrings["connStringTD"].ConnectionString));
            _main = main;
            laadWaarden();
        }

        private void laadWaarden() {
            txtKaartnummer.IsEnabled = false;
            txtKaartnummer.Text = Convert.ToString(_tankkaart.Kaartnummer);
            dpGeldigheidsdatum.SelectedDate = _tankkaart.Geldigheidsdatum;
            txtPincode.Text = _tankkaart.Pincode;
            if (_bestuurderManager.GeefAlleBestuurdersZonderTankkaarten() != null) {
                cbBestuurder.ItemsSource = _bestuurderManager.GeefAlleBestuurdersZonderTankkaarten().Select(x => x.Voornaam);
            } else {
                cbBestuurder.ItemsSource = null;
            }
            
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {

        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e) {
            _main.wpUserControl.Children.Remove(this);
        }
    }
}
