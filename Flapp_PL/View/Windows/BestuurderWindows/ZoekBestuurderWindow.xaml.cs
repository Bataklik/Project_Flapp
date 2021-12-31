using Flapp_PL.View.UserControls;
using System;
using System.Windows;

namespace Flapp_PL.View.Windows {
    public partial class ZoekBestuurderWindow : Window {
        MainWindow main;
        BestuurderUC bUC;
        bool heeftVoertuig;
        public ZoekBestuurderWindow(MainWindow main, BestuurderUC bUC) {
            InitializeComponent();
            this.main = main;
            this.bUC = bUC;
            heeftVoertuig = false;
        }

        private string toUpperFirstletter(string value) {
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtNaam.Text) && string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate == null) {
                main.Show();
                bUC.LaadBestuurders(heeftVoertuig);
                Close();
                return;
            }
            // Naam Voornaam Date
            else if (!string.IsNullOrWhiteSpace(txtNaam.Text) && !string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate != null) {
                main.Show();
                bUC.LaadAlleBestuurdersOpNaamVoornaamDate(toUpperFirstletter(txtNaam.Text), toUpperFirstletter(txtVoornaam.Text), (DateTime)dpGeboortedatum.SelectedDate, heeftVoertuig);
                Close();
                return;
            }
            // Naam Voornaam
            else if (!string.IsNullOrWhiteSpace(txtNaam.Text) && !string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate == null) {
                main.Show();
                bUC.LaadAlleBestuurdersOpNaamVoornaam(toUpperFirstletter(txtNaam.Text), toUpperFirstletter(txtVoornaam.Text), heeftVoertuig);
                Close();
                return;
            }
            // Naam
            else if (!string.IsNullOrWhiteSpace(txtNaam.Text) && string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate == null) {
                main.Show();
                bUC.LaadAlleBestuurdersOpNaam(toUpperFirstletter(txtNaam.Text), heeftVoertuig);
                Close();
                return;
            }
            // Voornaam
            else if (string.IsNullOrWhiteSpace(txtNaam.Text) && !string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate == null) {
                main.Show();
                bUC.LaadAlleBestuurdersOpVoornaam(toUpperFirstletter(txtVoornaam.Text), heeftVoertuig);
                Close();
                return;
            }
            // Date
            else if (string.IsNullOrWhiteSpace(txtNaam.Text) && string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate != null) {
                main.Show();
                bUC.LaadAlleBestuurdersOpDatum((DateTime)dpGeboortedatum.SelectedDate, heeftVoertuig);
                Close();
                return;
            }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void tbVoetuig_Checked(object sender, RoutedEventArgs e) {
            heeftVoertuig = true;
        }

        private void tbVoetuig_Unchecked(object sender, RoutedEventArgs e) {
            heeftVoertuig = false;
        }
    }
}
