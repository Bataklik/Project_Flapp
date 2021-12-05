using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Flapp_PL.View.Windows.TankkaartWindows {
    /// <summary>
    /// Interaction logic for TankkaartZoekBestuurderWindow.xaml
    /// </summary>
    public partial class TankkaartZoekBestuurderWindow : Window {
        private BestuurderManager _bestuurderManager;
        private TankkaartToevoegenWindow _ttw;
        private Bestuurder _bestuurder;

        public TankkaartZoekBestuurderWindow(TankkaartToevoegenWindow ttw) {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadWaarden();
            _ttw = ttw;
        }

        private void laadWaarden() {

        }

        private void VoegBestuurderToe_Click(object sender, RoutedEventArgs e) {
            try {
                List<Bestuurder> bestuurders = new List<Bestuurder>();
                bestuurders.Add((Bestuurder)lstBestuurders.SelectedItem);

                _ttw.lbBestuurder.ItemsSource = bestuurders;
                Close();
            }
            catch (Exception) { throw; }
        }

        private void btnZoekBestuurder_Click(object sender, RoutedEventArgs e) {

        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
