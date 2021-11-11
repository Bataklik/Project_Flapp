using Flapp_PL.View.UserControls;
using System;
using System.Windows;

namespace Flapp_PL.View.Windows
{
    public partial class ZoekBestuurderWindow : Window
    {
        public ZoekBestuurderWindow()
        {
            InitializeComponent();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            if (!string.IsNullOrWhiteSpace(txtNaam.Text) && !string.IsNullOrWhiteSpace(txtVoornaam.Text) && dpGeboortedatum.SelectedDate != null)
            {
                main.Show();
                main.wpUserControl.Children.Add(new BestuurderUC(txtNaam.Text, txtVoornaam.Text, (DateTime)dpGeboortedatum.SelectedDate));            
                Close();
                return;
            }
            MessageBox.Show("Velden zijn leeg!");
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
