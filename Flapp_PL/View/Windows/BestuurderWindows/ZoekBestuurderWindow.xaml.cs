using Flapp_PL.View.UserControls;
using System;
using System.Windows;

namespace Flapp_PL.View.Windows
{
    public partial class ZoekBestuurderWindow : Window
    {
        MainWindow main;
        BestuurderUC bUC;
        public ZoekBestuurderWindow(MainWindow main, BestuurderUC bUC)
        {
            InitializeComponent();
            this.main = main;
            this.bUC = bUC;
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNaam.Text) && string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate == null)
            {
                main.Show();
                bUC.LaadBestuurders();
                Close();
                return;
            }
            // Naam Voornaam Date
            else if (!string.IsNullOrWhiteSpace(txtNaam.Text) && !string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate != null)
            {
                main.Show();
                bUC.LaadAlleBestuurdersOpNaamVoornaamDate(txtNaam.Text, txtVoornaam.Text, (DateTime)dpGeboortedatum.SelectedDate);
                Close();
                return;
            }
            // Naam Voornaam
            else if (!string.IsNullOrWhiteSpace(txtNaam.Text) && !string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate == null)
            {
                main.Show();
                bUC.LaadAlleBestuurdersOpNaamVoornaam(txtNaam.Text,txtVoornaam.Text);
                Close();
                return;
            }
            // Naam
            else if (!string.IsNullOrWhiteSpace(txtNaam.Text) && string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate == null)
            {
                main.Show();
                bUC.LaadAlleBestuurdersOpNaam(txtNaam.Text);
                Close();
                return;
            }
            // Voornaam
            else if (string.IsNullOrWhiteSpace(txtNaam.Text) && !string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate == null)
            {
                main.Show();
                bUC.LaadAlleBestuurdersOpVoornaam(txtVoornaam.Text);
                Close();
                return;
            }
            // Date
            else if (string.IsNullOrWhiteSpace(txtNaam.Text) && string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate != null)
            {
                main.Show();
                bUC.LaadAlleBestuurdersOpDatum((DateTime)dpGeboortedatum.SelectedDate);
                Close();
                return;
            }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
