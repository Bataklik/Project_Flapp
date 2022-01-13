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
            Regex regex = new Regex(@"^[0-9]{4}$");
            e.Handled = regex.IsMatch(txtPincode.Text);
        }

        private void btnBestuurderbeheer_Click(object sender, RoutedEventArgs e) {
            TankkaartUpdateWindow ttw = this;
            new Bestuurderbeheer(ttw).ShowDialog();
        }

        private void laadWaarden() {
            try {
                txtKaartnummer.IsEnabled = false;
                txtKaartnummer.Text = $"{Tankkaart.Kaartnummer}";
                dpGeldigheidsdatum.SelectedDate = Tankkaart.Geldigheidsdatum;
                txtPincode.Text = $"{Tankkaart.Pincode}";
                if (Tankkaart.Geblokkeerd) cbGeblokkeerd.SelectedIndex = 0;
                cbGeblokkeerd.SelectedIndex = 1;
                cbBrandstoffen.ItemsSource = _brandstofManager.GeefAlleBrandstoffen();
                foreach (var b in Tankkaart.Brandstoffen) {
                    lbBrandstof.Items.Add(_brandstofManager.GeefBrandstof(b));
                    Brandstoffen.Add(_brandstofManager.GeefBrandstof(b));
                }

                if (Tankkaart.Bestuurder != null) lstBestuurder.Items.Add(Tankkaart.Bestuurder);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnVoegBrandstofToe_Click(object sender, RoutedEventArgs e) {
            try {
                Brandstof selected = _brandstofManager.GeefBrandstof((Brandstof)cbBrandstoffen.SelectedItem);
                if (selected == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
                if (lbBrandstof.Items.Contains(selected)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
                Brandstoffen.Add(selected);
                lbBrandstof.ItemsSource = Brandstoffen;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }      
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {
            try {
                bool geblokkeerd = cbGeblokkeerd.SelectedIndex == 0 ? geblokkeerd = true : geblokkeerd = false;
                Tankkaart.ZetKaartnummer(int.Parse(txtKaartnummer.Text));
                Tankkaart.ZetGeldigheidsdatum((DateTime)dpGeldigheidsdatum.SelectedDate);
                Tankkaart.ZetPincode(txtPincode.Text);
                Tankkaart.ZetGeblokkeerd(geblokkeerd);
                Tankkaart.ZetBestuurder(lstBestuurder.Items.Count > 0 ? (Bestuurder)lstBestuurder.Items[0] : null);
                Tankkaart.ZetBrandstoffen(Brandstoffen.ToList());
                _tankkaartManager.UpdateTankkaart(Tankkaart);
                MessageBox.Show("Updaten gelukt!");

                _tUC.laadTankkaarten();
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void VerwijderBestuurder_Click(object sender, RoutedEventArgs e) {
            try {
                if ((Bestuurder)lstBestuurder.SelectedItem == null) { MessageBox.Show("U heeft geen bestuurder gekozen!", "Geen bestuurder!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                if (MessageBox.Show("Bent u zeker?", "Opgelet!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                    lstBestuurder.Items.Remove((Bestuurder)lstBestuurder.SelectedItem);
                }
                _tUC.laadTankkaarten();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e) {
            try {
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
