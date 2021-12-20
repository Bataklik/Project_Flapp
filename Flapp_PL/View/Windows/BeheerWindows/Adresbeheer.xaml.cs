using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.BeheerWindows;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Flapp_PL.View.Windows.BestuurderWindows.BeheerWindows
{
    public partial class Adresbeheer : Window
    {
        private VoegBestuurderToe _parentWindow;
        private AdresManager _adresManager;
        private UpdateBestuurderWindow _parentUpdateWindow;

        public Adresbeheer(VoegBestuurderToe parentWindow)
        {
            InitializeComponent();

            _parentWindow = parentWindow;
            _adresManager = new AdresManager(new AdresRepo(Application.Current.Properties["User"].ToString()));

            LaadAdressen();
        }
        public Adresbeheer(UpdateBestuurderWindow parentWindow)
        {
            InitializeComponent();

            _parentUpdateWindow = parentWindow;
            _adresManager = new AdresManager(new AdresRepo(Application.Current.Properties["User"].ToString()));

            LaadAdressen();
        }

        public void LaadAdressen()
        {
            try
            {
                lstAdressen.ItemsSource = _adresManager.GeefAdressen();
                cbSteden.ItemsSource = _adresManager.GeefAlleSteden();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void miSelecteer_Click(object sender, RoutedEventArgs e)
        {
            if (lstAdressen.SelectedItems == null) { MessageBox.Show("U heeft geen adres geselecteerd!"); return; }
            if (_parentWindow == null)
            {
                _parentUpdateWindow.lstAdres.Items.Clear();
                _parentUpdateWindow.lstAdres.Items.Add((Adres)lstAdressen.SelectedItem);
            }
            else
            {
                _parentWindow.lstAdres.Items.Clear();
                _parentWindow.lstAdres.Items.Add((Adres)lstAdressen.SelectedItem);
            }
            Close();
        }
        private void miVoegToe_Click(object sender, RoutedEventArgs e)
        {
            new VoegAdresToe(this, _adresManager).ShowDialog();
        }
        private void miVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lstAdressen.SelectedItem == null) { MessageBox.Show("U heeft geen adres geselecteerd"); }
            try
            {
                _adresManager.VerwijderAdres((Adres)lstAdressen.SelectedItem);
                LaadAdressen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void cbSteden_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbSteden.SelectedItem == null) { MessageBox.Show("Geen Juiste stad aangeduid!"); return; }
            KeyValuePair<int, string> stad = (KeyValuePair<int, string>)cbSteden.SelectedItem;
            try { cbStraten.ItemsSource = _adresManager.GeefStratenStad(stad.Key, stad.Value); }
            catch (Exception) { throw; }
        }
        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            if (cbSteden.SelectedItem == null || cbStraten.SelectedItem == null) { MessageBox.Show("Geen Juiste stad of straat aangeduid!"); return; }
            KeyValuePair<int, string> stad = (KeyValuePair<int, string>)cbSteden.SelectedItem;
            lstAdressen.ItemsSource = _adresManager.ZoekAdressen(stad.Key, stad.Value, (string)cbStraten.SelectedItem);
        }
    }
}
