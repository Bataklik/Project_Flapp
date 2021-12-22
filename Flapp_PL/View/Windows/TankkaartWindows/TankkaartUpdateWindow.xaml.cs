using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using Flapp_PL.View.Windows.BeheerWindows;
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
        private Tankkaart _tankkaart;
        private TankkaartUC _tUC;
        private BestuurderManager _bestuurderManager;
        private BrandstofManager _brandstofManager;
        private TankkaartManager _tankkaartManager;

        public TankkaartUpdateWindow(Tankkaart t, TankkaartUC tUC) {
            InitializeComponent();
            _tankkaart = t;
            _tUC = tUC;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            laadWaarden();
        }

        private void btnBestuurderbeheer_Click(object sender, RoutedEventArgs e) {
            TankkaartUpdateWindow ttw = this;
            new Bestuurderbeheer(ttw).ShowDialog();
        }

        private void laadWaarden() {
            txtKaartnummer.Text = Convert.ToString(_tankkaart.Kaartnummer);
            txtKaartnummer.IsEnabled = false;
            dpGeldigheidsdatum.SelectedDate = _tankkaart.Geldigheidsdatum;
            txtPincode.Text = _tankkaart.Pincode;
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            if (_tankkaart.Bestuurder != null) {
                bestuurders.Add(_tankkaart.Bestuurder);
                lstBestuurder.ItemsSource = bestuurders;
            } 
            if (_tankkaart.Geblokkeerd) cbGeblokkeerd.SelectedIndex = 0;
            cbGeblokkeerd.SelectedIndex = 1;
            cbBrandstoffen.ItemsSource = _brandstofManager.GeefAlleBrandstoffen();
            lbBrandstof.ItemsSource = _tankkaart.Brandstoffen;
        }

        private void btnVoegBrandstofToe_Click(object sender, RoutedEventArgs e) {
            if ((Brandstof)cbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (lbBrandstof.Items.Contains((Brandstof)cbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
            lbBrandstof.Items.Add((Brandstof)cbBrandstoffen.SelectedItem);
        }

        private void btnVerwijderBrandstof_Click(object sender, RoutedEventArgs e) {
            if ((Brandstof)cbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            lbBrandstof.Items.Remove((Brandstof)cbBrandstoffen.SelectedItem);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {
            try {
                DateTime geldigheidsdatum = (DateTime)dpGeldigheidsdatum.SelectedDate;
                int kaartnummer = Int32.Parse(txtKaartnummer.Text);
                string pincode = txtPincode.Text;
                bool geblokkeerd = false;
                if (cbGeblokkeerd.SelectedIndex == 0) { geblokkeerd = true;  }
                //List<Brandstof> brandstoffen = lbBrandstof.Items.Cast<Brandstof>().ToList();
                Tankkaart t = new Tankkaart(kaartnummer ,geldigheidsdatum, pincode, geblokkeerd);
                _tankkaartManager.UpdateTankkaart(t);
                MessageBox.Show("Updaten gelukt!");
                _tUC.lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaarten().Select(x => x.Value).ToList();
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
