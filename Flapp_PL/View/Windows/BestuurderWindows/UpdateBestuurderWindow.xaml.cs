using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Flapp_PL.View.Windows.BestuurderWindows
{
    public partial class UpdateBestuurderWindow : Window
    {
        public ObservableCollection<Rijbewijs> Rijbewijzen { get; set; } = new ObservableCollection<Rijbewijs>();


        private BestuurderManager _bestuurderManager;
        private Bestuurder _bestuurder;
        private RijbewijsManager _rijbewijsManager;
        //private AdresManager _adresManager;
        public UpdateBestuurderWindow(Bestuurder bestuurder)
        {
            InitializeComponent();
            _bestuurder = bestuurder;
            laadBestuurder();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _rijbewijsManager = new RijbewijsManager(new RijbewijsRepo(Application.Current.Properties["User"].ToString()));
            //_adresManager = new AdresManager(new AdresRepo(Application.Current.Properties["User"].ToString()));
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {
            _bestuurderManager.UpdateBestuurder(_bestuurder);
        }

        private void cbRijbewijzen_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cbRijbewijzen.ItemsSource = _rijbewijsManager.GeefAlleRijbewijzen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cbGeslacht_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> geslachten = new List<string> { "Man", "Vrouw" };
            var box = sender as ComboBox;
            box.ItemsSource = geslachten;
            box.SelectedIndex = 0;
        }

        private void btnAddRijbewijs_Click(object sender, RoutedEventArgs e)
        {
            if ((Rijbewijs)cbRijbewijzen.SelectedItem == null) { MessageBox.Show("U heeft geen rijbewijs aangeduid!"); return; }
            if (lstRijbewijzen.Items.Contains((Rijbewijs)cbRijbewijzen.SelectedItem)) { MessageBox.Show("Rijbewijs staat al op de lijst!"); return; }
            Rijbewijzen.Add((Rijbewijs)cbRijbewijzen.SelectedItem);
            lstRijbewijzen.ItemsSource = Rijbewijzen;
        }
        private void btnRemoveRijbewijs_Click(object sender, RoutedEventArgs e)
        {
            if ((Rijbewijs)lstRijbewijzen.SelectedItem == null) { MessageBox.Show("U heeft geen rijbewijs aangeduid!"); return; }
            if (!lstRijbewijzen.Items.Contains((Rijbewijs)lstRijbewijzen.SelectedItem)) { MessageBox.Show("Rijbewijs staat niet al op de lijst!"); return; }
            Rijbewijzen.Remove((Rijbewijs)lstRijbewijzen.SelectedItem);
            lstRijbewijzen.ItemsSource = Rijbewijzen;
        }

        private void txtPostcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void laadBestuurder()
        {
            txtNaam.Text = _bestuurder.Naam;
            txtVoornaam.Text = _bestuurder.Voornaam;
            if (_bestuurder.Geslacht == Geslacht.M) { cbGeslacht.SelectedIndex = 0; }
            else { cbGeslacht.SelectedIndex = 1; }
            dpGeboorte.DataContext = _bestuurder.Geboortedatum;
            txtRijksregister.Text = _bestuurder.Rijksregisternummer;
            lstRijbewijzen.ItemsSource = _bestuurder.Rijbewijzen;
            Rijbewijzen = new ObservableCollection<Rijbewijs>(_bestuurder.Rijbewijzen); 

            if (_bestuurder.Adres !=null) {
                txtStraat.Text = _bestuurder.Adres.Straat;
                txtHuisnummer.Text = _bestuurder.Adres.Huisnummer;
                txtStad.Text = _bestuurder.Adres.Stad;
                txtPostcode.Text = _bestuurder.Adres.Postcode.ToString();
            }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
