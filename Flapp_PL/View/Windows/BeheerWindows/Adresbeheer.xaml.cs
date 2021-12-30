using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.BeheerWindows;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Flapp_PL.View.Windows.BestuurderWindows.BeheerWindows {
    public partial class Adresbeheer : Window {
        private VoegBestuurderToe _parentWindow;
        private AdresManager _adresManager;
        private UpdateBestuurderWindow _parentUpdateWindow;

        public Adresbeheer(VoegBestuurderToe parentWindow) {
            InitializeComponent();

            _parentWindow = parentWindow;
            _adresManager = new AdresManager(new AdresRepo(Application.Current.Properties["User"].ToString()));

            LaadAdressen();
        }
        public Adresbeheer(UpdateBestuurderWindow parentWindow) {
            InitializeComponent();

            _parentUpdateWindow = parentWindow;
            _adresManager = new AdresManager(new AdresRepo(Application.Current.Properties["User"].ToString()));

            LaadAdressen();
        }


        #region Click Methods
        private void miSelecteer_Click(object sender, RoutedEventArgs e) {
            if (lstAdressen.SelectedItems == null) { MessageBox.Show("U heeft geen adres geselecteerd!"); return; }
            if (_parentWindow == null) {
                _parentUpdateWindow.lstAdres.Items.Clear();
                _parentUpdateWindow.lstAdres.Items.Add((Adres)lstAdressen.SelectedItem);
            }
            else {
                _parentWindow.lstAdres.Items.Clear();
                _parentWindow.lstAdres.Items.Add((Adres)lstAdressen.SelectedItem);
            }
            Close();
        }
        private void miVoegToe_Click(object sender, RoutedEventArgs e) {
            new VoegAdresToe(this, _adresManager).ShowDialog();
        }
        private void miVerwijderen_Click(object sender, RoutedEventArgs e) {
            if (lstAdressen.SelectedItem == null) { MessageBox.Show("U heeft geen adres geselecteerd"); }
            try {
                _adresManager.VerwijderAdres((Adres)lstAdressen.SelectedItem);
                LaadAdressen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            lstAdressen.ItemsSource = _adresManager.ZoekAdressen(toUpperFirstletter(txtSteden.Text), toUpperFirstletter(txtStraten.Text));
        }
        #endregion

        #region Helpers
        private string toUpperFirstletter(string value) {
            if (string.IsNullOrWhiteSpace(value)) { return ""; }
            return char.ToUpper(value[0]) + value.Substring(1).ToLower();
        }
        public void LaadAdressen() {
            try {
                lstAdressen.ItemsSource = _adresManager.GeefAdressen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion
    }
}
