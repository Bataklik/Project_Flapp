using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using Flapp_PL.View.Windows.BeheerWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Flapp_PL.View.Windows.TankkaartWindows {
    /// <summary>
    /// Interaction logic for ZoekTankkaartWindow.xaml
    /// </summary>
    public partial class ZoekTankkaartWindow : Window {
        private MainWindow _main;
        private TankkaartUC _tUC;
        private TankkaartManager _tankkaartManager;

        public ZoekTankkaartWindow(MainWindow main, TankkaartUC tUC) {
            _main = main;
            _tUC = tUC;
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            InitializeComponent();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            try {
                List<Tankkaart> tankkaarten = new List<Tankkaart>();
                int? kaartnummer = txtKaartnummer.Text == "" ? kaartnummer = null : kaartnummer = int.Parse(txtKaartnummer.Text.Trim());
                DateTime? geldigheidsdatum = dpGeldigheidsdatum.SelectedDate;
                Bestuurder bestuurder = lstBestuurder.Items.Count > 0 ? bestuurder = (Bestuurder)lstBestuurder.Items[0] : bestuurder = null;
                int? bestuurderid = bestuurder != null ? bestuurderid = bestuurder.Id : bestuurderid = null;
                string naam = bestuurder != null ? naam = bestuurder.Naam : naam = "";
                string voornaam = bestuurder != null ? voornaam = bestuurder.Voornaam : voornaam = "";
                DateTime? geboortedatum = bestuurder != null ? geboortedatum = bestuurder.Geboortedatum : geboortedatum = null;
                string  rijksregister = bestuurder != null ? rijksregister = bestuurder.Rijksregisternummer : rijksregister = "";

                foreach (KeyValuePair<int, Tankkaart> kvp in _tankkaartManager.GeefTankkaarten(kaartnummer, geldigheidsdatum, bestuurderid, naam, voornaam, geboortedatum, rijksregister)) {
                    tankkaarten.Add(kvp.Value);
                }
                _tUC.lstTankkaarten.ItemsSource = tankkaarten;
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e) {
            try {
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnBestuurderbeheer_Click(object sender, RoutedEventArgs e) {
            new Bestuurderbeheer(this).ShowDialog();
        }
        
        private void VerwijderBestuurder_Click(object sender, RoutedEventArgs e) {
            try {

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
