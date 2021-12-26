using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows;
using Flapp_PL.View.Windows.BestuurderWindows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls {
    public partial class BestuurderUC : UserControl {
        private BestuurderManager _bestuurderManager;
        private MainWindow _main;

        //public BestuurderUC()
        //{
        //    InitializeComponent();
        //    _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));

        //    LaadBestuurders();
        //}
        public BestuurderUC(MainWindow main) {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            _main = main;
            LaadBestuurders();
        }

        public void LaadBestuurders() {
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            try { foreach (KeyValuePair<int, Bestuurder> v in _bestuurderManager.GeefAlleBestuurders()) { bestuurders.Add(v.Value); } }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            lstbBestuurders.ItemsSource = bestuurders;
        }
        public void LaadAlleBestuurdersOpNaam(string naam, bool heeftVoertuig) {
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            try {
                foreach (Bestuurder v in _bestuurderManager.GeefAlleBestuurdersOpNaam(naam, heeftVoertuig).Values.ToList()) {
                    bestuurders.Add(v);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            lstbBestuurders.ItemsSource = bestuurders;
        }
        public void LaadAlleBestuurdersOpVoornaam(string voornaam) {
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            try {
                foreach (Bestuurder v in _bestuurderManager.GeefAlleBestuurdersOpVoornaam(voornaam).Values.ToList()) {
                    bestuurders.Add(v);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            lstbBestuurders.ItemsSource = bestuurders;
        }
        public void LaadAlleBestuurdersOpDatum(DateTime date) {
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            try {
                foreach (Bestuurder v in _bestuurderManager.GeefAlleBestuurdersOpDatum(date).Values.ToList()) {
                    bestuurders.Add(v);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            lstbBestuurders.ItemsSource = bestuurders;
        }
        public void LaadAlleBestuurdersOpNaamVoornaam(string naam, string voornaam) {
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            try {
                foreach (Bestuurder v in _bestuurderManager.GeefAlleBestuurdersOpNaamVoornaam(naam, voornaam).Values.ToList()) {
                    bestuurders.Add(v);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            lstbBestuurders.ItemsSource = bestuurders;
        }
        public void LaadAlleBestuurdersOpNaamVoornaamDate(string naam, string voornaam, DateTime date, bool heeftVoertuig) {
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            try {
                foreach (Bestuurder v in _bestuurderManager.GeefAlleBestuurdersOpNaamVoornaamDate(naam, voornaam, date).Values.ToList()) {
                    bestuurders.Add(v);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            lstbBestuurders.ItemsSource = bestuurders;
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
            try { _bestuurderManager.VerwijderBestuurder((Bestuurder)lstbBestuurders.SelectedItem); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { LaadBestuurders(); }
        }
    }
}
