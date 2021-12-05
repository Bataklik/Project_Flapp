using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.Windows.BestuurderWindows
{
    public partial class VoegBestuurderToe : Window
    {
        private BestuurderManager _bestuurderManager;
        private RijbewijsManager _rijbewijsManager;
        private AdresManager _adresManager;
        public VoegBestuurderToe()
        {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _rijbewijsManager = new RijbewijsManager(new RijbewijsRepo(Application.Current.Properties["User"].ToString()));
            _adresManager = new AdresManager(new AdresRepo(Application.Current.Properties["User"].ToString()));
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaam.Text) || string.IsNullOrEmpty(txtVoornaam.Text) || cbGeslacht.SelectedItem == null || dpGeboorte.SelectedDate == null || string.IsNullOrWhiteSpace(txtRijksregister.Text)) { MessageBox.Show("Er zijn velden niet ingevuld!", "Velden leeg!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            Geslacht s = cbGeslacht.SelectedItem.ToString() == "Man" ? Geslacht.M : Geslacht.V;
            try
            {
                Adres a = null;
                Bestuurder bestuurder = null;
                if (!string.IsNullOrWhiteSpace(txtStraat.Text) || !string.IsNullOrWhiteSpace(txtHuisnummer.Text) || !string.IsNullOrWhiteSpace(txtStad.Text) || !string.IsNullOrWhiteSpace(txtPostcode.Text))
                {
                    a = new Adres(txtStraat.Text, txtHuisnummer.Text, txtStad.Text, int.Parse(txtPostcode.Text));
                    if (_adresManager.BestaatAdres(a)) { a = _adresManager.GeefAdres(a); }
                    else
                    {
                        _adresManager.VoegAdresToe(a);
                        a = _adresManager.GeefAdres(a);
                    }
                    bestuurder = new Bestuurder(txtNaam.Text, txtVoornaam.Text, s, a, dpGeboorte.Text, txtRijksregister.Text, lstRijbewijzen.Items.Cast<Rijbewijs>().ToList());
                    // Rijbewijs toevoegen aan DB
                    _bestuurderManager.VoegBestuurderToe(bestuurder);
                }
                else
                {
                    bestuurder = new Bestuurder(txtNaam.Text, txtVoornaam.Text, s, dpGeboorte.Text, txtRijksregister.Text, lstRijbewijzen.Items.Cast<Rijbewijs>().ToList());
                    _bestuurderManager.VoegBestuurderToeZonderAdres(bestuurder);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddRijbewijs_Click(object sender, RoutedEventArgs e)
        {
            if ((Rijbewijs)cbRijbewijzen.SelectedItem == null) { MessageBox.Show("U heeft geen rijbewijs aangeduid!"); return; }
            if (lstRijbewijzen.Items.Contains((Rijbewijs)cbRijbewijzen.SelectedItem)) { MessageBox.Show("Rijbewijs staat al op de lijst!"); return; }
            lstRijbewijzen.Items.Add((Rijbewijs)cbRijbewijzen.SelectedItem);
        }
        private void btnRemoveRijbewijs_Click(object sender, RoutedEventArgs e)
        {
            if ((Rijbewijs)lstRijbewijzen.SelectedItem == null) { MessageBox.Show("U heeft geen rijbewijs aangeduid!"); return; }
            if (!lstRijbewijzen.Items.Contains((Rijbewijs)lstRijbewijzen.SelectedItem)) { MessageBox.Show("Rijbewijs staat niet al op de lijst!"); return; }
            lstRijbewijzen.Items.Remove((Rijbewijs)lstRijbewijzen.SelectedItem);
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

        private void txtPostcode_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
