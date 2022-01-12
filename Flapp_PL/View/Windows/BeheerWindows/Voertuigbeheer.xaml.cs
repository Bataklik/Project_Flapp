using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.BestuurderWindows;
using Flapp_PL.View.Windows.VoertuigWindow;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.Windows.BeheerWindows {
    public partial class Voertuigbeheer : Window {
        private VoegBestuurderToe _parentWindow;
        private UpdateBestuurderWindow _parentUpdateWindow;

        private VoertuigManager _voertuigManager;

        public Voertuigbeheer(VoegBestuurderToe parentWindow) {
            InitializeComponent();
            _parentWindow = parentWindow;
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            LaadVoertuigen();
        }

        public Voertuigbeheer(UpdateBestuurderWindow parentWindow) {
            InitializeComponent();
            _parentUpdateWindow = parentWindow;
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            LaadVoertuigen();
        }

        #region Click Methods
        private void miVoegToe_Click(object sender, RoutedEventArgs e) {
            try
            {
                new VoertuigToevoegen(this).ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void miVerwijderen_Click(object sender, RoutedEventArgs e) {
            if (lstVoertuigen.SelectedItem == null) { MessageBox.Show("U heeft geen tankkaart geselecteerd"); }
            try {
                _voertuigManager.VerwijderVoertuig((Voertuig)lstVoertuigen.SelectedItem);
                LaadVoertuigen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void miSelecteer_Click(object sender, RoutedEventArgs e) {
            try
            {
                if (lstVoertuigen.SelectedItems == null) { MessageBox.Show("U heeft geen voertuig geselecteerd!"); return; }
                if (_parentWindow == null)
                {
                    _parentUpdateWindow.lstVoertuig.Items.Clear();
                    _parentUpdateWindow.lstVoertuig.Items.Add((Voertuig)lstVoertuigen.SelectedItem);
                }
                else
                {
                    _parentWindow.lstVoertuig.Items.Clear();
                    _parentWindow.lstVoertuig.Items.Add((Voertuig)lstVoertuigen.SelectedItem);
                }
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            //if (cbMerk.SelectedItem == null || cbModel.SelectedItem == null) { MessageBox.Show("Geen Juiste merk of model aangeduid!"); return; }
            lstVoertuigen.ItemsSource = _voertuigManager.ZoekVoertuigZonderBestuurder(toUpperFirstletter(cmbMerk.Text), toUpperFirstletter(cmbModel.Text), txtNummerplaat.Text).Values;
        }
        #endregion

        #region Helpers
        private string toUpperFirstletter(string value) {
            if (string.IsNullOrWhiteSpace(value)) { return ""; }
            return char.ToUpper(value[0]) + value[1..].ToLower();
        }

        private void LaadVoertuigen() {
            try {
                lstVoertuigen.ItemsSource = _voertuigManager.GeefVoertuigenZonderBestuurder().Values;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion

        private void cmbMerk_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<string> merken = new(_voertuigManager.GeefMerken());
                cmbMerk.ItemsSource = merken;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cmbModel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cmbMerk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbMerk.SelectedIndex > -1)
                {
                    ObservableCollection<string> modellen = new(_voertuigManager.GeefModellenMerk(cmbMerk.SelectedItem.ToString()));
                    cmbModel.ItemsSource = modellen;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }            
        }
    }
}
