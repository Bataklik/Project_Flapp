using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows;
using Flapp_PL.View.Windows.BestuurderWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls {
    public partial class BestuurderUC : UserControl {
        private BestuurderManager _bestuurderManager;
        private MainWindow _main;

        public BestuurderUC(MainWindow main) {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _main = main;
            LaadBestuurders();
        }

        public void LaadBestuurders(string naam = null, string voornaam = null, DateTime? geboorte = null, bool heeftVoertuig = false) {
            try { lstbBestuurders.ItemsSource = _bestuurderManager.GeefAlleBestuurders(naam, voornaam, geboorte, heeftVoertuig).Values.ToList(); }
            catch (Exception) { throw; }
        }
        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            BestuurderUC bUC = this;
            new ZoekBestuurderWindow(_main, bUC).ShowDialog();
        }
        private void btnVoegToe_Click(object sender, RoutedEventArgs e) {
            new VoegBestuurderToe(this).ShowDialog();
        }
        private void UpdateBestuurder_Click(object sender, RoutedEventArgs e) {
            if ((Bestuurder)lstbBestuurders.SelectedItem == null) { MessageBox.Show("U heeft geen bestuurder gekozen!", "Geen bestuurder!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            new UpdateBestuurderWindow((Bestuurder)lstbBestuurders.SelectedItem, this).ShowDialog();
        }
        private void VerwijderBestuurder_Click(object sender, RoutedEventArgs e) {
            if ((Bestuurder)lstbBestuurders.SelectedItem == null) { MessageBox.Show("U heeft geen bestuurder gekozen!", "Geen bestuurder!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            Bestuurder teVerwijderen = (Bestuurder)lstbBestuurders.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Wilt u zeker Bestuurder: \n{teVerwijderen.Naam} {teVerwijderen.Voornaam}, {teVerwijderen.Adres}\n verwijderen?", "Bestuurder Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.No) { return; }
            try { _bestuurderManager.VerwijderBestuurder(teVerwijderen); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { LaadBestuurders(); }
        }
    }
}
