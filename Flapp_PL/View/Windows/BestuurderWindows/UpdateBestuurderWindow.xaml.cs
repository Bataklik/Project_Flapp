﻿using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using Flapp_PL.View.Windows.BeheerWindows;
using Flapp_PL.View.Windows.BestuurderWindows.BeheerWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Flapp_PL.View.Windows.BestuurderWindows {
    public partial class UpdateBestuurderWindow : Window {
        public ObservableCollection<Rijbewijs> Rijbewijzen { get; set; } = new ObservableCollection<Rijbewijs>();
        private BestuurderManager _bestuurderManager;
        public Bestuurder Bestuurder;
        private RijbewijsManager _rijbewijsManager;

        private BestuurderUC _parentWindow;

        private string toUpperFirstletter(string value) {
            return char.ToUpper(value[0]) + value.Substring(1).ToLower();
        }

        public UpdateBestuurderWindow(Bestuurder bestuurder, BestuurderUC parentWindow) {
            InitializeComponent();
            Bestuurder = bestuurder;
            _parentWindow = parentWindow;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _rijbewijsManager = new RijbewijsManager(new RijbewijsRepo(Application.Current.Properties["User"].ToString()));
            laadBestuurder();

        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e) {
            try {
                Bestuurder.ZetNaam(toUpperFirstletter(txtNaam.Text));
                Bestuurder.ZetVoornaam(toUpperFirstletter(txtVoornaam.Text));
                Bestuurder.ZetGeslacht((string)cbGeslacht.SelectedItem == "Man" ? Geslacht.M : Geslacht.V);
                Bestuurder.ZetGeboortedatum(dpGeboorte.SelectedDate.ToString());
                Bestuurder.ZetRijksregisternummer(txtRijksregister.Text);
                Bestuurder.ZetRijbewijsLijst(lstRijbewijzen.Items.Cast<Rijbewijs>().ToList());


                if (lstAdres.Items.Count > 0) { Bestuurder.ZetVoertuig((Voertuig)lstVoertuig.Items[0]); }
                if (lstVoertuig.Items.Count > 0) { Bestuurder.ZetVoertuig((Voertuig)lstVoertuig.Items[0]); }
                if (lstTankkaart.Items.Count > 0) { Bestuurder.ZetTankkaart((Tankkaart)lstTankkaart.Items[0]); }

                _bestuurderManager.UpdateBestuurder(Bestuurder);
                MessageBox.Show("Bestuurder is geüpdatet!", "Updaten gelukt!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                _parentWindow.LaadBestuurders();
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void cbRijbewijzen_Loaded(object sender, RoutedEventArgs e) {
            try {
                cbRijbewijzen.ItemsSource = _rijbewijsManager.GeefAlleRijbewijzen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void cbGeslacht_Loaded(object sender, RoutedEventArgs e) {
            List<string> geslachten = new List<string> { "Man", "Vrouw" };
            var box = sender as ComboBox;
            box.ItemsSource = geslachten;
            box.SelectedIndex = 0;
        }
        private void btnAddRijbewijs_Click(object sender, RoutedEventArgs e) {
            if ((Rijbewijs)cbRijbewijzen.SelectedItem == null) { MessageBox.Show("U heeft geen rijbewijs aangeduid!"); return; }
            if (lstRijbewijzen.Items.Contains((Rijbewijs)cbRijbewijzen.SelectedItem)) { MessageBox.Show("Rijbewijs staat al op de lijst!"); return; }
            Rijbewijzen.Add((Rijbewijs)cbRijbewijzen.SelectedItem);
            lstRijbewijzen.ItemsSource = Rijbewijzen;
        }
        private void btnRemoveRijbewijs_Click(object sender, RoutedEventArgs e) {
            if ((Rijbewijs)lstRijbewijzen.SelectedItem == null) { MessageBox.Show("U heeft geen rijbewijs aangeduid!"); return; }
            if (!lstRijbewijzen.Items.Contains((Rijbewijs)lstRijbewijzen.SelectedItem)) { MessageBox.Show("Rijbewijs staat niet al op de lijst!"); return; }
            Rijbewijzen.Remove((Rijbewijs)lstRijbewijzen.SelectedItem);
            lstRijbewijzen.ItemsSource = Rijbewijzen;
        }
        private void txtPostcode_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnAdresbeheer_Click(object sender, RoutedEventArgs e) {
            new Adresbeheer(this).ShowDialog();
        }
        private void btnVoertuigbeheer_Click(object sender, RoutedEventArgs e) {
            new Voertuigbeheer(this).ShowDialog();
        }
        private void btnTankkaartbeheer_Click(object sender, RoutedEventArgs e) {
            new Tankaartbeheer(this).ShowDialog();
        }

        private void laadBestuurder() {
            txtNaam.Text = Bestuurder.Naam;
            txtVoornaam.Text = Bestuurder.Voornaam;
            if (Bestuurder.Geslacht == Geslacht.M) { cbGeslacht.SelectedIndex = 0; }
            else { cbGeslacht.SelectedIndex = 1; }
            dpGeboorte.Text = Bestuurder.Geboortedatum.ToShortDateString();
            txtRijksregister.Text = Bestuurder.Rijksregisternummer;
            lstRijbewijzen.ItemsSource = Bestuurder.Rijbewijzen;
            Rijbewijzen = new ObservableCollection<Rijbewijs>(Bestuurder.Rijbewijzen);

            if (Bestuurder.Adres != null) { lstAdres.Items.Add(Bestuurder.Adres); }
            if (Bestuurder.Voertuig != null) { lstVoertuig.Items.Add(Bestuurder.Voertuig); }
            if (Bestuurder.Tankkaart != null) { lstTankkaart.Items.Add(Bestuurder.Tankkaart); }

        }
        private void btnAnnuleer_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
