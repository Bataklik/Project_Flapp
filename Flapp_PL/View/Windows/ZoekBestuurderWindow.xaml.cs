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
            if (!string.IsNullOrWhiteSpace(txtNaam.Text) && !string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate != null)
            {
                main.Show();
                main.wpUserControl.Children.Remove(bUC);
                main.wpUserControl.Children.Add(new BestuurderUC(txtNaam.Text, txtVoornaam.Text, (DateTime)dpGeboortedatum.SelectedDate));            
                Close();
                return;
            }
            MessageBox.Show("Velden zijn leeg!");
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
