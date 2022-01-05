using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using Flapp_PL.View.Windows.BeheerWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public ObservableCollection<Brandstof> Brandstoffen { get; set; } = new ObservableCollection<Brandstof>();
        private Tankkaart Tankkaart;
        private TankkaartUC _tUC;
        private BestuurderManager _bestuurderManager;
        private BrandstofManager _brandstofManager;
        private TankkaartManager _tankkaartManager;

        public TankkaartUpdateWindow(Tankkaart t, TankkaartUC tUC) {
            InitializeComponent();
            Tankkaart = t;
            _tUC = tUC;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            laadWaarden();
        }

        private void txtPincode_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnBestuurderbeheer_Click(object sender, RoutedEventArgs e) {
            if (lstBestuurder.Items.Count == 0) {
                TankkaartUpdateWindow ttw = this;
                new Bestuurderbeheer(ttw).ShowDialog();
            } else {
                MessageBox.Show("Gelieve eerst de bestuurder te verwijderen!");
            }
            
        }

        private void laadWaarden() {
            txtKaartnummer.IsEnabled = false;
            txtKaartnummer.Text = $"{Tankkaart.Kaartnummer}";
            dpGeldigheidsdatum.SelectedDate = Tankkaart.Geldigheidsdatum;
            txtPincode.Text = $"{Tankkaart.Pincode}";
            if (Tankkaart.Geblokkeerd) cbGeblokkeerd.SelectedIndex = 0;
            cbGeblokkeerd.SelectedIndex = 1;
            cbBrandstoffen.ItemsSource = _brandstofManager.GeefAlleBrandstoffen();
            lbBrandstof.ItemsSource = Tankkaart.Brandstoffen;
            Brandstoffen = new ObservableCollection<Brandstof>(Tankkaart.Brandstoffen);

            if (Tankkaart.Bestuurder != null) lstBestuurder.Items.Add(Tankkaart.Bestuurder);
        }

        private void btnVoegBrandstofToe_Click(object sender, RoutedEventArgs e) {
            if ((Brandstof)cbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (lbBrandstof.Items.Contains((Brandstof)cbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
            Brandstoffen.Add((Brandstof)cbBrandstoffen.SelectedItem);
            lbBrandstof.ItemsSource = Brandstoffen;
        }

        private void btnVerwijderBrandstof_Click(object sender, RoutedEventArgs e) {
            if ((Brandstof)cbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen rijbewijs aangeduid!"); return; }
            if (!lbBrandstof.Items.Contains((Brandstof)cbBrandstoffen.SelectedItem)) { MessageBox.Show("Rijbewijs staat niet al op de lijst!"); return; }
            Brandstoffen.Remove((Brandstof)cbBrandstoffen.SelectedItem);
            lbBrandstof.ItemsSource = Brandstoffen;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {
            try {
                Tankkaart.ZetKaartnummer(int.Parse(txtKaartnummer.Text));
                Tankkaart.ZetGeldigheidsdatum((DateTime)dpGeldigheidsdatum.SelectedDate);
                Tankkaart.ZetPincode(txtPincode.Text);
                bool geblokkeerd = false;
                if (cbGeblokkeerd.SelectedIndex == 0) { geblokkeerd = true;  }
                Tankkaart.ZetGeblokkeerd(geblokkeerd);
                if (lstBestuurder.Items.Count > 0) Tankkaart.ZetBestuurder((Bestuurder)lstBestuurder.Items[0]);
                else { Tankkaart.ZetBestuurder(null); }
                List<Brandstof> lbBrandstoffen = new List<Brandstof>();
                foreach (var b in lbBrandstof.Items.Cast<Brandstof>().ToList()) {
                    lbBrandstoffen.Add(_brandstofManager.GeefBrandstof(b));
                }
                Tankkaart.ZetBrandstoffen(lbBrandstoffen);
                _tankkaartManager.UpdateTankkaart(Tankkaart);
                _brandstofManager.VerwijderBrandstofBijTankkaart(Tankkaart.Kaartnummer);
                _brandstofManager.VoegBrandstofToeAanTankkaart(Tankkaart.Kaartnummer, Tankkaart.Brandstoffen);
                if (Tankkaart.Bestuurder != null) _bestuurderManager.VoegTankkaartToeAanBestuurder(Tankkaart);
                MessageBox.Show("Updaten gelukt!");

                _tUC.lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaarten().Values;
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void VerwijderBestuurder_Click(object sender, RoutedEventArgs e) {
            try {
                if ((Bestuurder)lstBestuurder.SelectedItem == null) { MessageBox.Show("U heeft geen bestuurder gekozen!", "Geen bestuurder!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                if (MessageBox.Show("Bent u zeker?", "Opgelet!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                    _bestuurderManager.VerwijderTankkaartVanBestuurder((Bestuurder)lstBestuurder.SelectedItem);
                    lstBestuurder.Items.Remove((Bestuurder)lstBestuurder.SelectedItem);
                }
                _tUC.laadTankkaarten();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
