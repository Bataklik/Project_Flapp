using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.BeheerWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Flapp_PL.View.Windows.VoertuigWindows {
    public partial class VoertuigUpdaten : Window {
        private VoertuigManager _voertuigManager;
        private BrandstofManager _brandstofmanager;
        private VoertuigTypeManager _voertuigTypeManager;
        private BestuurderManager _bestuurderManager;
        private Voertuig _voertuig;
        private int aantalDeuren;
        public ObservableCollection<Brandstof> Brandstoffen { get; set; } = new ObservableCollection<Brandstof>();
        public VoertuigUpdaten(Voertuig v) {
            InitializeComponent();
            _voertuig = v;
            _brandstofmanager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            _voertuigTypeManager = new VoertuigTypeManager(new VoertuigTypeRepo(Application.Current.Properties["User"].ToString()));
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadVoertuig();
            laadMerk();
        }
        private string toUpperFirstletter(string value) {
            return char.ToUpper(value[0]) + value.Substring(1).ToLower();
        }
        public void laadVoertuig() {
            lstBrandtof.ItemsSource = _voertuig.Brandstof;
            //Brandstoffen = new ObservableCollection<Brandstof>(_voertuig.Brandstof);
            txtId.Text = $"{_voertuig.VoertuigID}";
            cmbMerk.Text = _voertuig.Merk;
            cmbModel.Text = _voertuig.Model;
            txtChassis.Text = _voertuig.ChassisNummer;
            cmbType.SelectedItem = _voertuig.VoertuigType;
            txtNummerplaat.Text = _voertuig.Nummerplaat;
            txtKleur.Text = _voertuig.Kleur;
            txtDeuren.Text = $"{_voertuig.Aantaldeuren}";
            aantalDeuren = Convert.ToInt32(txtDeuren.Text);
            if (_voertuig.Bestuurder != null) { lstBestuurder.Items.Add(_voertuig.Bestuurder); }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e) {
            try {
                Voertuig v = new Voertuig(Convert.ToInt32(txtId.Text), toUpperFirstletter(cmbMerk.Text), toUpperFirstletter(cmbModel.Text), txtChassis.Text, txtNummerplaat.Text, lstBrandtof.Items.Cast<Brandstof>().ToList(), cmbType.Text, txtKleur.Text, Convert.ToInt32(txtDeuren.Text));
                if (v.Brandstof == null) { MessageBox.Show("Reselect uw brandstof!"); return; }
                else {
                    if (lstBestuurder.Items.Count > 0) {
                        Bestuurder b = (Bestuurder)lstBestuurder.Items[0];
                        v.ZetBestuurder((Bestuurder)lstBestuurder.Items[0]);
                        b.ZetVoertuig(v);
                        _bestuurderManager.VoegVoertuigToeAanBestuurder(b);
                    }
                    _voertuigManager.UpdateVoertuig(v);
                    _brandstofmanager.VerwijderBrandstofBijVoertuig(v.VoertuigID);
                    _brandstofmanager.VoegBrandstofToeAanVoertuig(v.VoertuigID, v.Brandstof);
                    MessageBox.Show("Updaten Gelukt!");
                    Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnAnnuleren_Click(object sender, RoutedEventArgs e) {
            Close();
        }
        private void cmbBrandstoffen_Loaded(object sender, RoutedEventArgs e) {
            try {
                cmbBrandstoffen.ItemsSource = _brandstofmanager.GeefAlleBrandstoffen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnAddBrandstof_Click(object sender, RoutedEventArgs e) {
            if ((Brandstof)cmbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (lstBrandtof.Items.Contains((Brandstof)cmbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
            Brandstoffen.Add((Brandstof)cmbBrandstoffen.SelectedItem);
            lstBrandtof.ItemsSource = Brandstoffen;
        }
        private void btnRemoveBrandstof_Click(object sender, RoutedEventArgs e) {
            if ((Brandstof)lstBrandtof.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (!lstBrandtof.Items.Contains((Brandstof)lstBrandtof.SelectedItem)) { MessageBox.Show("Brandstof staat niet op de lijst!"); return; }
            Brandstoffen.Remove((Brandstof)lstBrandtof.SelectedItem);
            lstBrandtof.ItemsSource = Brandstoffen;
        }
        private void laadMerk() {
            ObservableCollection<string> merken = new(_voertuigManager.GeefMerken());
            merken.Insert(0, "");
            //cmbMerk.SelectedIndex = 0;
            cmbMerk.ItemsSource = merken;
        }
        private void cmbMerk_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            if (cmbMerk.SelectedIndex == 0) {
                //cmbModel.IsEnabled = false;
                cmbModel.ItemsSource = null;
            }
            else {
                //cmbModel.IsEnabled = true;
                ObservableCollection<string> modellen = new(_voertuigManager.GeefModellenMerk(cmbMerk.SelectedItem.ToString()));
                modellen.Insert(0, "");
                cmbModel.ItemsSource = modellen;
                cmbModel.SelectedIndex = 0;
                //merk = cmbMerk.SelectedItem.ToString();
                //model = null;
            }
        }
        private void btnPlusDeur_Click(object sender, RoutedEventArgs e) {
            aantalDeuren = Convert.ToInt32(txtDeuren.Text);
            aantalDeuren += 1;
            txtDeuren.Text = aantalDeuren.ToString();
        }
        private void btnMindeur_Click(object sender, RoutedEventArgs e) {
            aantalDeuren = Convert.ToInt32(txtDeuren.Text);
            aantalDeuren -= 1;
            txtDeuren.Text = aantalDeuren.ToString();
        }

        private void cmbType_Loaded(object sender, RoutedEventArgs e) {
            try {
                IReadOnlyList<string> types = _voertuigTypeManager.GeefAlleVoertuigTypes();
                ObservableCollection<string> ts = new();
                foreach (var type in types) {
                    ts.Add(type);
                }
                cmbType.ItemsSource = ts;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }

        private void cmbModel_Loaded(object sender, RoutedEventArgs e) {
            if (cmbMerk.SelectedIndex <= 0) {
                //cmbModel.IsEnabled = false;
                cmbModel.ItemsSource = null;
            }
            else {
                //cmbModel.IsEnabled = true;
                ObservableCollection<string> modellen = new(_voertuigManager.GeefModellenMerk(cmbMerk.SelectedItem.ToString()));
                modellen.Insert(0, "");
                cmbModel.ItemsSource = modellen;
                cmbModel.SelectedIndex = 0;
                //merk = cmbMerk.SelectedItem.ToString();
                //model = null;
            }
        }

        private void btnVoertuigbeheer_Click(object sender, RoutedEventArgs e) {
            new Bestuurderbeheer(this).ShowDialog();
        }
    }
}
