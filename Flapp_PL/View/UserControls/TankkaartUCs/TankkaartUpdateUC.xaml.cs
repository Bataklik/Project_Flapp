using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls.TankkaartUCs
{
    public partial class TankkaartUpdateUC : UserControl {
        private Tankkaart _tankkaart;
        private BestuurderManager _bestuurderManager;
        private TankkaartManager _tankkaartManager;
        private BrandstofManager _brandstofManager;
        private MainWindow _main;
        private TankkaartUC _tUC;

        public TankkaartUpdateUC(Tankkaart t, MainWindow main, TankkaartUC tUC) {
            InitializeComponent();
            _tankkaart = t;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            _main = main;
            _tUC = tUC;
            laadWaarden();
        }

        private void laadWaarden() {
            txtKaartnummer.IsEnabled = false;
            txtKaartnummer.Text = Convert.ToString(_tankkaart.Kaartnummer);
            dpGeldigheidsdatum.SelectedDate = _tankkaart.Geldigheidsdatum;
            txtPincode.Text = _tankkaart.Pincode;

            if (_bestuurderManager.GeefAlleBestuurdersZonderTankkaarten() != null) { cbBestuurder.ItemsSource = _bestuurderManager.GeefAlleBestuurdersZonderTankkaarten().Select(x => x.Voornaam); } 
            else { cbBestuurder.ItemsSource = null; }

            cbBrandstoftype.ItemsSource = _brandstofManager.GeefAlleBrandstoffen();

            string[] geblokkeerd = { "Ja", "Nee" };
            cbGeblokkeerd.ItemsSource = geblokkeerd;
            if (_tankkaart.Geblokkeerd) { cbGeblokkeerd.SelectedItem = geblokkeerd[0]; }
            cbGeblokkeerd.SelectedItem = geblokkeerd[1];
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {
            int kaartnummer = _tankkaart.Kaartnummer;
            DateTime geldigheidsdatum = Convert.ToDateTime(dpGeldigheidsdatum.SelectedDate);
            string pincode = txtPincode.Text;
            Brandstof b = (Brandstof)cbBrandstoftype.SelectedItem;
            bool geblokkeerd = false;
            if (cbGeblokkeerd.SelectedItem.ToString() == "Ja") geblokkeerd = true;
            Tankkaart t = new Tankkaart(kaartnummer, geldigheidsdatum, pincode, geblokkeerd);
            try {
                _tankkaartManager.UpdateTankkaart(t);
                MessageBox.Show("Updaten gelukt!");
                _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
                //_tUC.lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaarten().ToList();               
            } catch (Exception) { throw; }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e) {
            _main.wpUserControl.Children.Remove(this);
        }
    }
}
