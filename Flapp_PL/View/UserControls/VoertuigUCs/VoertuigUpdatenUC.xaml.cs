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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flapp_PL.View.UserControls.VoertuigUCs
{
    /// <summary>
    /// Interaction logic for VoertuigUpdatenUC.xaml
    /// </summary>
    public partial class VoertuigUpdatenUC : UserControl
    {
        MainWindow _main;
        //private Voertuig _voertuig;
        int voertuigid;
        private VoertuigManager _voertuigManager;
        private ObservableCollection<Brandstof> _brandstoffen;
        public VoertuigUpdatenUC(Voertuig v, MainWindow main)
        {
            InitializeComponent();
            _main = main;
            voertuigid = v.VoertuigID;            
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            laadGegevens();
        }

        public void laadGegevens()
        {
            txtVoertuigID.IsEnabled = false;
            Voertuig voertuig = _voertuigManager.GeefVoertuigDoorID(voertuigid);
            _brandstoffen = new ObservableCollection<Brandstof>(voertuig.Brandstof);
            txtVoertuigID.Text = $"{voertuig.VoertuigID}";
            txtMerk.Text = voertuig.Merk;
            txtModel.Text = voertuig.Model;
            txtChassisnummer.Text = voertuig.ChassisNummer;
            txtVoertuigType.Text = voertuig.VoertuigType;
            txtNummerplaat.Text = voertuig.Nummerplaat;
            //txtKleur.Text = voertuig.Kleur;
            //txtDeuren.Text = $"{voertuig.Aantaldeuren}";
            //if (voertuig.Bestuurder != null)
            //{
            //    txtBestuurder.Text = $"{voertuig.Bestuurder.Naam}";
            //}
            //else
            //{
            //    txtBestuurder.Text = "none";
            //}
            //lstBrandstoftypes.ItemsSource = _brandstoffen;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            _main.wpUserControl.Children.Remove(this);
        }

    }
}
