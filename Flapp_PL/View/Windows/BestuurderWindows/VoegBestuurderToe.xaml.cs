﻿using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using Flapp_PL.View.Windows.BeheerWindows;
using Flapp_PL.View.Windows.BestuurderWindows.BeheerWindows;
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
        private BestuurderUC _parentWindow;

        public VoegBestuurderToe(BestuurderUC bestuurderUC)
        {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _rijbewijsManager = new RijbewijsManager(new RijbewijsRepo(Application.Current.Properties["User"].ToString()));
            _parentWindow = bestuurderUC;
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaam.Text) || string.IsNullOrEmpty(txtVoornaam.Text) || cbGeslacht.SelectedItem == null || dpGeboorte.SelectedDate == null || string.IsNullOrWhiteSpace(txtRijksregister.Text)) { MessageBox.Show("Er zijn velden niet ingevuld!", "Velden leeg!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            Geslacht s = cbGeslacht.SelectedItem.ToString() == "Man" ? Geslacht.M : Geslacht.V;
            Bestuurder bestuurder = null;
            try
            {
                bestuurder = new Bestuurder(txtNaam.Text, txtVoornaam.Text, s, dpGeboorte.Text, txtRijksregister.Text, lstRijbewijzen.Items.Cast<Rijbewijs>().ToList());

                if (lstAdres.Items.Count > 0) { bestuurder.ZetAdres((Adres)lstAdres.Items[0]); }
                if (lstVoertuig.Items.Count > 0) { bestuurder.ZetVoertuig((Voertuig)lstVoertuig.Items[0]); }
                if (lstTankkaart.Items.Count > 0) { bestuurder.ZetTankkaart((Tankkaart)lstTankkaart.Items[0]); }

                bestuurder.ZetId(_bestuurderManager.VoegBestuurderToe(bestuurder));
                if (bestuurder.Rijbewijzen.Count > 0) { _rijbewijsManager.VoegRijbewijzenToeBestuurder(bestuurder.Id, bestuurder.Rijbewijzen); }
                MessageBox.Show("Bestuurder is Toegevoegd!", "Toevoegen gelukt!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                ClearInputs();
                _parentWindow.LaadBestuurders();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Bestuurder Toevoegen.", MessageBoxButton.OK, MessageBoxImage.Error); }
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
        private void ClearInputs()
        {
            txtNaam.Text = string.Empty;
            txtVoornaam.Text = string.Empty;
            cbGeslacht.SelectedIndex = 0;
            txtRijksregister.Text = string.Empty;
            dpGeboorte.Text = string.Empty;
            cbRijbewijzen.SelectedIndex = -1;
            lstRijbewijzen.Items.Clear();
        }

        private void btnAdresbeheer_Click(object sender, RoutedEventArgs e)
        {
            new Adresbeheer(this).ShowDialog();
        }
        private void btnVoertuigbeheer_Click(object sender, RoutedEventArgs e)
        {
            new Voertuigbeheer(this).ShowDialog();
        }
        private void btnTankkaartbeheer_Click(object sender, RoutedEventArgs e)
        {
            new Tankaartbeheer(this).ShowDialog();
        }
    }
}
