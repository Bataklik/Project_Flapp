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
            return string.IsNullOrEmpty(value) ? "" : char.ToUpper(value[0]) + value[1..].ToLower();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            bUC.LaadBestuurders(toUpperFirstletter(txtNaam.Text), toUpperFirstletter(txtVoornaam.Text), dpGeboortedatum.SelectedDate, heeftVoertuig);
            main.Show();
            Close();
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
