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
        private ObservableCollection<Brandstof> _brandstoffen;
        public VoertuigUpdaten(int voertuigId)
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            Voertuig voertuig = _voertuigManager.GeefVoertuigDoorID(voertuigId);
            _brandstoffen = new ObservableCollection<Brandstof>(voertuig.Brandstof);
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
            lstBrandstoftypes.ItemsSource = _brandstoffen;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Voertuig v = new Voertuig(Convert.ToInt32(txtVoertuigId.Text), txtMerk.Text, txtModel.Text, txtChassisNummer.Text, txtNummerplaat.Text, new List<Brandstof>(), txtVoertuigtype.Text, txtKleur.Text, Convert.ToInt32(txtDeuren.Text));
            try
            {
                _voertuigManager.UpdateVoertuig(v);
                MessageBox.Show("Updaten Gelukt!");
                Close();

            }
            catch (Exception ex) { throw; }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
