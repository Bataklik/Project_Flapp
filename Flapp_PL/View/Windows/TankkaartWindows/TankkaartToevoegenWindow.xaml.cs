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
            try {
                dpGeldigheidsdatum.SelectedDate = DateTime.Now.AddYears(2);
                dpGeldigheidsdatum.IsEnabled = false;
                cbBrandstoffen.ItemsSource = _brandstofManager.GeefAlleBrandstoffen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtPincode_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) {
            Regex regex = new Regex(@"[0-9]{4}");
            Regex regex2 = new Regex("[0-9]");

            e.Handled = !regex2.IsMatch(e.Text);
            if (txtPincode.Text.Length > 3) {
                e.Handled = regex.IsMatch(txtPincode.Text);
            }
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e) {
            try {
                if (dpGeldigheidsdatum.SelectedDate != null && !string.IsNullOrWhiteSpace(txtPincode.Text) && lbBrandstof.Items.Count > 0) {
                    Tankkaart t = null;
                    DateTime geldigheidsdatum = (DateTime)dpGeldigheidsdatum.SelectedDate;
                    string pincode = txtPincode.Text.Trim();
                    bool geblokkeerd = false;
                    if (cbGeblokkeerd.SelectedIndex == 0) { geblokkeerd = true; }
                    Bestuurder bestuurder = null;
                    if (lstBestuurder.Items.Count > 0) bestuurder = (Bestuurder)lstBestuurder.Items[0];
                    List<Brandstof> brandstoffen = lbBrandstof.Items.Cast<Brandstof>().ToList();
                    t = new Tankkaart(geldigheidsdatum, pincode, geblokkeerd, brandstoffen, bestuurder);
                    t.ZetKaartnummer(_tankkaartManager.VoegTankkaartToe(t));
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
            try {
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }        
        }

        private void btnVoegBrandstofToe_Click(object sender, RoutedEventArgs e) {
            try {

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            if ((Brandstof)cbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (lbBrandstof.Items.Contains((Brandstof)cbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
            Brandstoffen.Add((Brandstof)cbBrandstoffen.SelectedItem);
            lbBrandstof.ItemsSource = Brandstoffen;
        }

        private void btnVerwijderBrandstof_Click(object sender, RoutedEventArgs e) {
            try {
                if ((Brandstof)cbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
                if (!lbBrandstof.Items.Contains((Brandstof)cbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat niet al op de lijst!"); return; }
                Brandstoffen.Remove((Brandstof)cbBrandstoffen.SelectedItem);
                lbBrandstof.ItemsSource = Brandstoffen;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }         
        }

        private void btnBestuurderbeheer_Click(object sender, RoutedEventArgs e) {
            TankkaartToevoegenWindow ttw = this;
            new Bestuurderbeheer(ttw).ShowDialog();
        }
    }
}
