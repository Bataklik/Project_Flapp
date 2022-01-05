using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using Flapp_PL.View.Windows.BeheerWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Flapp_PL.View.Windows.TankkaartWindows {

    public partial class TankkaartToevoegenWindow : Window {
        public ObservableCollection<Brandstof> Brandstoffen { get; set; } = new ObservableCollection<Brandstof>();
        private TankkaartManager _tankkaartManager;
        private BrandstofManager _brandstofManager;
        private BestuurderManager _bestuurderManager;
        private Tankaartbeheer _parentWindow;
        private TankkaartUC _tUC;

        public TankkaartToevoegenWindow(Tankaartbeheer parentWindow) {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _parentWindow = parentWindow;
            laadWaarden();
        }
        public TankkaartToevoegenWindow(TankkaartUC tUC) {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _tUC = tUC;
            laadWaarden();
        }

        private void laadWaarden() {
            dpGeldigheidsdatum.SelectedDate = DateTime.Now.AddYears(2);
            dpGeldigheidsdatum.IsEnabled = false;
            try {
                cbBrandstoffen.ItemsSource = _brandstofManager.GeefAlleBrandstoffen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtPincode_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e) {
            try {
                if (dpGeldigheidsdatum.SelectedDate != null && !string.IsNullOrWhiteSpace(txtPincode.Text) && lbBrandstof.Items.Count > 0) {
                    Tankkaart t = null;
                    DateTime geldigheidsdatum = (DateTime)dpGeldigheidsdatum.SelectedDate;
                    string pincode = txtPincode.Text;
                    bool geblokkeerd = false;
                    if (cbGeblokkeerd.SelectedIndex == 0) { geblokkeerd = true; }
                    Bestuurder bestuurder = null;
                    if (lstBestuurder.Items.Count > 0) bestuurder = (Bestuurder)lstBestuurder.Items[0];
                    List<Brandstof> brandstoffen = lbBrandstof.Items.Cast<Brandstof>().ToList();
                    t = new Tankkaart(geldigheidsdatum, pincode, geblokkeerd, brandstoffen, bestuurder);
                    if (t.Bestuurder.Voertuig != null) {
                        if (t.Brandstoffen[0].Naam == t.Bestuurder.Voertuig.Brandstof[0].Naam) {
                            VoegTankkaartToe(t);
                        }
                        else {
                            MessageBox.Show("Brandstoffen kunnen niet gecombineerd worden met het voertuig!");
                        }
                    }
                    else { VoegTankkaartToe(t); }
                    MessageBox.Show("Tankkaart toegevoegd!");
                    if (_tUC != null) { _tUC.laadTankkaarten(); }
                    else { _parentWindow.LaadTankkaarten(); }
                    Close();
                }
                else { MessageBox.Show("Velden zijn leeg!"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e) {
            Close();
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

        private void btnBestuurderbeheer_Click(object sender, RoutedEventArgs e) {
            TankkaartToevoegenWindow ttw = this;
            new Bestuurderbeheer(ttw).ShowDialog();
        }

        private void VoegTankkaartToe(Tankkaart t) {
            t.ZetKaartnummer(_tankkaartManager.VoegTankkaartToe(t));
            _brandstofManager.VoegBrandstofToeAanTankkaart(t.Kaartnummer, t.Brandstoffen);
            if (t.Bestuurder != null) _bestuurderManager.VoegTankkaartToeAanBestuurder(t);
        }
    }
}
