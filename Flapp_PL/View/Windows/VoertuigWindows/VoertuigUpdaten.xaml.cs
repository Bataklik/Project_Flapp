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
            try
            {
                lstBrandtof.ItemsSource = _voertuig.Brandstof;
                Brandstoffen = new ObservableCollection<Brandstof>(_voertuig.Brandstof);
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            
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
                    }
                    _voertuigManager.UpdateVoertuig(v);
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
            try
            {
                if ((Brandstof)cmbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
                if (lstBrandtof.Items.Contains((Brandstof)cmbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
                Brandstoffen.Add((Brandstof)cmbBrandstoffen.SelectedItem);
                lstBrandtof.ItemsSource = Brandstoffen;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnRemoveBrandstof_Click(object sender, RoutedEventArgs e) {
            try
            {
                if ((Brandstof)lstBrandtof.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
                if (!lstBrandtof.Items.Contains((Brandstof)lstBrandtof.SelectedItem)) { MessageBox.Show("Brandstof staat niet op de lijst!"); return; }
                Brandstoffen.Remove((Brandstof)lstBrandtof.SelectedItem);
                lstBrandtof.ItemsSource = Brandstoffen;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            
        }
        private void laadMerk() {
            try
            {
                ObservableCollection<string> merken = new(_voertuigManager.GeefMerken());
                merken.Insert(0, "");
                cmbMerk.ItemsSource = merken;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }            
        }
        private void cmbMerk_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            try
            {
                if (cmbMerk.SelectedIndex == 0)
                {
                    cmbModel.ItemsSource = null;
                }
                else
                {
                    ObservableCollection<string> modellen = new(_voertuigManager.GeefModellenMerk(cmbMerk.SelectedItem.ToString()));
                    modellen.Insert(0, "");
                    cmbModel.ItemsSource = modellen;
                    cmbModel.SelectedIndex = 0;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }
        private void btnPlusDeur_Click(object sender, RoutedEventArgs e) {
            try
            {
                 aantalDeuren = Convert.ToInt32(txtDeuren.Text);
                switch (aantalDeuren)
                {
                    case (3):
                        aantalDeuren = 5;
                        break;
                    case (5):
                        aantalDeuren = 7;
                        break;
                    default:
                        break;
                }
                txtDeuren.Text = aantalDeuren.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }            
        }
        private void btnMindeur_Click(object sender, RoutedEventArgs e) {
            try
            {
                aantalDeuren = Convert.ToInt32(txtDeuren.Text);
                switch (aantalDeuren)
                {
                    case (7):
                        aantalDeuren = 5;
                        break;
                    case (5):
                        aantalDeuren = 3;
                        break;
                    default:
                        break;
                }
                //aantalDeuren = 5;
                txtDeuren.Text = aantalDeuren.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }            
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
            try
            {
                if (cmbMerk.SelectedIndex <= 0)
                {
                    cmbModel.ItemsSource = null;
                }
                else
                {
                    ObservableCollection<string> modellen = new(_voertuigManager.GeefModellenMerk(cmbMerk.SelectedItem.ToString()));
                    cmbModel.ItemsSource = modellen;
                    cmbModel.SelectedIndex = 0;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }            
        }

        private void btnVoertuigbeheer_Click(object sender, RoutedEventArgs e) {
            try
            {
                new Bestuurderbeheer(this).ShowDialog();
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }

            
        }
        private void miDeselecterenBestuurder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstBestuurder.Items.Count < 1) { MessageBox.Show("Er is geen bestuurder geselecteerd!", "Geen Bestuurder!", MessageBoxButton.OK, MessageBoxImage.Information); return; }
                MessageBoxResult result = MessageBox.Show("Wilt u deze bestuurder verwijderen van het voertuig?", "Niet Selecteren!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    _bestuurderManager.UpdateBestuurder_voertuigId(_voertuig.VoertuigID);
                    lstBestuurder.Items.Clear();
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }            
        }
    }
}
