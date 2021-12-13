using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
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

namespace Flapp_PL.View.Windows.VoertuigWindows
{
    /// <summary>
    /// Interaction logic for VoertuigUpdaten.xaml
    /// </summary>
    public partial class VoertuigUpdaten : Window
    {
        private VoertuigManager _voertuigManager;
        private BrandstofManager _brandstofmanager;        
        private Voertuig _voertuig;
        public ObservableCollection<Brandstof> Brandstoffen { get; set; } = new ObservableCollection<Brandstof>();       
        public VoertuigUpdaten(Voertuig v)
        {            
            InitializeComponent();
            _voertuig = v;
            laadVoertuig();
            _brandstofmanager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));            
        }
        public void laadVoertuig()
        {
            //Voertuig voertuig = _voertuigManager.GeefVoertuigDoorID(_voertuig.VoertuigID);
            lstBrandstoftypes.ItemsSource = _voertuig.Brandstof;
            Brandstoffen = new ObservableCollection<Brandstof>(_voertuig.Brandstof);
            txtVoertuigId.Text = $"{_voertuig.VoertuigID}";
            txtMerk.Text = _voertuig.Merk;
            txtModel.Text = _voertuig.Model;
            txtChassisNummer.Text = _voertuig.ChassisNummer;
            txtVoertuigtype.Text = _voertuig.VoertuigType;
            txtNummerplaat.Text = _voertuig.Nummerplaat;
            txtKleur.Text = _voertuig.Kleur;
            txtDeuren.Text = $"{_voertuig.Aantaldeuren}";
            if (_voertuig.Bestuurder != null)
            {
                txtBestuurder.Text = $"{_voertuig.Bestuurder.Naam}";
            }
            else
            {
                txtBestuurder.Text = "none";
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //List<Brandstof> b = new List<Brandstof>(lstBrandstoftypes.Items.Cast<Brandstof>().ToList());
            
            try
            {
                Voertuig v = new Voertuig(Convert.ToInt32(txtVoertuigId.Text), txtMerk.Text, txtModel.Text, txtChassisNummer.Text, txtNummerplaat.Text, lstBrandstoftypes.Items.Cast<Brandstof>().ToList(), txtVoertuigtype.Text, txtKleur.Text, Convert.ToInt32(txtDeuren.Text));
                _voertuigManager.UpdateVoertuig(v);
                _brandstofmanager.VerwijderBrandstofBijVoertuig(v.VoertuigID);
                _brandstofmanager.VoegBrandstofToeAanVoertuig(v.VoertuigID, v.Brandstof);
                MessageBox.Show("Updaten Gelukt!");
                Close();
            }
            catch (Exception ex) { throw; }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cmbBrandstoffen_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbBrandstoffen.ItemsSource = _brandstofmanager.GeefAlleBrandstoffen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAddBrandstof_Click(object sender, RoutedEventArgs e)
        {
            if ((Brandstof)cmbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (lstBrandstoftypes.Items.Contains((Brandstof)cmbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
            Brandstoffen.Add((Brandstof)cmbBrandstoffen.SelectedItem);
            lstBrandstoftypes.ItemsSource = Brandstoffen;
        }

        private void btnRemoveBrandstof_Click(object sender, RoutedEventArgs e)
        {
            if ((Brandstof)lstBrandstoftypes.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (!lstBrandstoftypes.Items.Contains((Brandstof)lstBrandstoftypes.SelectedItem)) { MessageBox.Show("Brandstof staat niet op de lijst!"); return; }
            Brandstoffen.Remove((Brandstof)lstBrandstoftypes.SelectedItem);
            lstBrandstoftypes.ItemsSource = Brandstoffen;
        }
    }
}
