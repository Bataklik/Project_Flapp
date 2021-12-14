using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
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
        private Tankkaart tankkaart;
        private BestuurderManager _bestuurderManager;
        private BrandstofManager _brandstofManager;

        public TankkaartUpdateWindow(Tankkaart t) {
            InitializeComponent();
            tankkaart = t;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            laadWaarden();
        }

        private void btnBestuurder_Click(object sender, RoutedEventArgs e) {
            TankkaartUpdateWindow tuw = this;
            TankkaartZoekBestuurderWindow tzbw = new TankkaartZoekBestuurderWindow(tuw);
            tzbw.ShowDialog();
        }

        private void laadWaarden() {
            txtKaartnummer.Text = Convert.ToString(tankkaart.Kaartnummer);
            txtKaartnummer.IsEnabled = false;
            dpGeldigheidsdatum.SelectedDate = tankkaart.Geldigheidsdatum;
            txtPincode.Text = tankkaart.Pincode;
            if (tankkaart.Bestuurder != null) lbBestuurder.ItemsSource = _bestuurderManager.GeefAlleBestuurdersOpNaam(tankkaart.Bestuurder.Naam); //Nog veranderen
            if (tankkaart.Geblokkeerd) cbGeblokkeerd.SelectedIndex = 0;
            cbGeblokkeerd.SelectedIndex = 1;
            cbBrandstoffen.ItemsSource = _brandstofManager.GeefAlleBrandstoffen();
            lbBrandstof.ItemsSource = tankkaart.Brandstoffen;
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
