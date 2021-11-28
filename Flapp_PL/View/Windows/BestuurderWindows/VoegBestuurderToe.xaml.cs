using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Windows;

namespace Flapp_PL.View.Windows.BestuurderWindows
{
    public partial class VoegBestuurderToe : Window
    {
        private BestuurderManager _bestuurderManager;
        public VoegBestuurderToe()
        {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaam.Text) || string.IsNullOrEmpty(txtVoornaam.Text) || cbGeslacht.SelectedItem == null || dpGeboorte.SelectedDate == null || string.IsNullOrWhiteSpace(txtRijksregister.Text)) { MessageBox.Show("Er zijn velden niet ingevuld!", "Velden leeg!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            try
            {
                //_bestuurderManager.VoegBestuurderToe(new Bestuurder();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
