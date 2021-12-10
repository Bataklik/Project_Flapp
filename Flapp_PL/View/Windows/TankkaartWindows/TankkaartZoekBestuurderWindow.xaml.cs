using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Flapp_PL.View.Windows.TankkaartWindows {
    /// <summary>
    /// Interaction logic for TankkaartZoekBestuurderWindow.xaml
    /// </summary>
    public partial class TankkaartZoekBestuurderWindow : Window {
        private BestuurderManager _bestuurderManager;
        private TankkaartToevoegenWindow _ttw;
        private TankkaartUpdateWindow _tuw;
        private Bestuurder _bestuurder;

        public TankkaartZoekBestuurderWindow(TankkaartToevoegenWindow ttw) {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadWaarden();
            _ttw = ttw;
        }

        public TankkaartZoekBestuurderWindow(TankkaartUpdateWindow tuw) {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadWaarden();
            _tuw = tuw;
        }

        private void laadWaarden() {
            lstBestuurders.ItemsSource = _bestuurderManager.GeefAlleBestuurders().Select(x => x.Value).ToList();
        }

        private void VoegBestuurderToe_Click(object sender, RoutedEventArgs e) {
            try {
                List<Bestuurder> bestuurders = new List<Bestuurder>();
                bestuurders.Add((Bestuurder)lstBestuurders.SelectedItem);

                if (_ttw != null) {
                    _ttw.lbBestuurder.ItemsSource = bestuurders;
                }
                else {
                    _tuw.lbBestuurder.ItemsSource = bestuurders;
                }
                
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
