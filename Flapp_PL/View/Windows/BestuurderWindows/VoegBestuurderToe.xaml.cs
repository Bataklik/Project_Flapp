using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Flapp_PL.View.Windows.BestuurderWindows
{
    public partial class VoegBestuurderToe : Window
    {
        private BestuurderManager _bestuurderManager;
        private RijbewijsManager _rijbewijsManager;
        public VoegBestuurderToe()
        {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _rijbewijsManager = new RijbewijsManager(new RijbewijsRepo(Application.Current.Properties["User"].ToString()));

        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaam.Text) || string.IsNullOrEmpty(txtVoornaam.Text) || cbGeslacht.SelectedItem == null || dpGeboorte.SelectedDate == null || string.IsNullOrWhiteSpace(txtRijksregister.Text)) { MessageBox.Show("Er zijn velden niet ingevuld!", "Velden leeg!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            Geslacht s = cbGeslacht.SelectedItem.ToString() == "Man" ? Geslacht.M : Geslacht.V;

            Bestuurder bestuurder = new Bestuurder(txtNaam.Text, txtVoornaam.Text, s, dpGeboorte.Text, txtRijksregister.Text, lstRijbewijzen.Items.Cast<Rijbewijs>().ToList());
            try
            {
                MessageBox.Show(bestuurder.ToString());
                //_bestuurderManager.VoegBestuurderToe(new Bestuurder();
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


    }
}
