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
        private List<Brandstof> _brandstoffen = new List<Brandstof>();
        private ObservableCollection<Brandstof> fuelTypes;// = new ObservableCollection<Brandstof>();
        public VoertuigUpdaten(Voertuig v)
        {
            InitializeComponent();
            _brandstofmanager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            Voertuig voertuig = _voertuigManager.GeefVoertuigDoorID(v.VoertuigID);

            //fuelTypes = new ObservableCollection<Brandstof>(voertuig.geefBrandstoffen());
            //foreach (var b in fuelTypes)
            //{
            //    lstBrandstoftypes.Items.Add(b);
            //}
            
            txtVoertuigId.Text = $"{voertuig.VoertuigID}";
            txtMerk.Text = voertuig.Merk;
            txtModel.Text = voertuig.Model;
            txtChassisNummer.Text = voertuig.ChassisNummer;
            txtVoertuigtype.Text = voertuig.VoertuigType;
            txtNummerplaat.Text = voertuig.Nummerplaat;
            txtKleur.Text = voertuig.Kleur;
            txtDeuren.Text = $"{voertuig.Aantaldeuren}";
            if (voertuig.Bestuurder != null)
            {
                txtBestuurder.Text = $"{voertuig.Bestuurder.Naam}";
            }
            else
            {
                txtBestuurder.Text = "none";
            }
            //lstBrandstoftypes.ItemsSource = v.Brandstof;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            List<Brandstof> b = new List<Brandstof>(lstBrandstoftypes.Items.Cast<Brandstof>().ToList());
            Voertuig v = new Voertuig(Convert.ToInt32(txtVoertuigId.Text), txtMerk.Text, txtModel.Text, txtChassisNummer.Text, txtNummerplaat.Text, b, txtVoertuigtype.Text, txtKleur.Text, Convert.ToInt32(txtDeuren.Text));
            try
            {
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
            lstBrandstoftypes.Items.Add((Brandstof)cmbBrandstoffen.SelectedItem);
        }

        private void btnRemoveBrandstof_Click(object sender, RoutedEventArgs e)
        {
            if ((Brandstof)lstBrandstoftypes.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (!lstBrandstoftypes.Items.Contains((Brandstof)lstBrandstoftypes.SelectedItem)) { MessageBox.Show("Brandstof staat niet op de lijst!"); return; }
            lstBrandstoftypes.Items.Remove((Brandstof)cmbBrandstoffen.SelectedItem);
        }
    }
}
