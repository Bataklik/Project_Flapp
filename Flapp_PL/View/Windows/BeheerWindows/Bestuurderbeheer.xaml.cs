using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.TankkaartWindows;
using Flapp_PL.View.Windows.VoertuigWindow;
using Flapp_PL.View.Windows.VoertuigWindows;
using System;
using System.Windows;

namespace Flapp_PL.View.Windows.BeheerWindows {
    /// <summary>
    /// Interaction logic for Bestuurderbeheer.xaml
    /// </summary>

    public partial class Bestuurderbeheer : Window {
        private VoertuigToevoegen _voertuigToevoegenWindow;
        private VoertuigUpdaten _voertuigUpdatenWindow;

        private TankkaartToevoegenWindow _tankkaartToevoegenWindow;
        private TankkaartUpdateWindow _tankkaartUpdateWindow;
        private ZoekTankkaartWindow _zoekTankkaartWindow;

        private BestuurderManager _bestuurderManager;

        public Bestuurderbeheer(VoertuigToevoegen voertuigToevoegenWindow) {
            InitializeComponent();
            _voertuigToevoegenWindow = voertuigToevoegenWindow;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadBestuurders();
        }
        public Bestuurderbeheer(VoertuigUpdaten voertuigUpdatenWindow) {
            InitializeComponent();
            _voertuigUpdatenWindow = voertuigUpdatenWindow;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadBestuurders();
        }
        public Bestuurderbeheer(TankkaartToevoegenWindow tankkaartToevoegenWindow) {
            InitializeComponent();
            _tankkaartToevoegenWindow = tankkaartToevoegenWindow;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadBestuurders();
        }
        public Bestuurderbeheer(TankkaartUpdateWindow tankkaartUpdateWindow) {
            InitializeComponent();
            _tankkaartUpdateWindow = tankkaartUpdateWindow;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadBestuurders();
        }
        public Bestuurderbeheer(ZoekTankkaartWindow zoekTankkaartWindow) {
            InitializeComponent();
            _zoekTankkaartWindow = zoekTankkaartWindow;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadBestuurders();
        }

        public void laadBestuurders() {
            try {
                if (_tankkaartToevoegenWindow != null || _tankkaartUpdateWindow != null) { lstBestuurder.ItemsSource = _bestuurderManager.GeefAlleBestuurdersZonderTankkaarten().Values; }
                if (_voertuigToevoegenWindow != null || _voertuigUpdatenWindow != null) { lstBestuurder.ItemsSource = _bestuurderManager.GeefAlleBestuurdersZonderVoertuig().Values; }
                if (_zoekTankkaartWindow != null) { lstBestuurder.ItemsSource = _bestuurderManager.GeefAlleBestuurdersZonderTankkaarten().Values; }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }



        private void miSelecteer_Click(object sender, RoutedEventArgs e) {
            try {
                if (lstBestuurder.SelectedItem == null) { MessageBox.Show("U heeft geen bestuurder geselecteerd!"); return; }

                if (_voertuigToevoegenWindow == null && _tankkaartToevoegenWindow == null && _tankkaartUpdateWindow == null && _zoekTankkaartWindow == null) {
                    _voertuigUpdatenWindow.lstBestuurder.Items.Clear();
                    _voertuigUpdatenWindow.lstBestuurder.Items.Add((Bestuurder)lstBestuurder.SelectedItem);
                }
                else if (_voertuigUpdatenWindow == null && _tankkaartToevoegenWindow == null && _tankkaartUpdateWindow == null && _zoekTankkaartWindow == null) {
                    _voertuigToevoegenWindow.lstBestuurder.Items.Clear();
                    _voertuigToevoegenWindow.lstBestuurder.Items.Add((Bestuurder)lstBestuurder.SelectedItem);
                }
                else if (_voertuigToevoegenWindow == null && _voertuigUpdatenWindow == null && _tankkaartUpdateWindow == null && _zoekTankkaartWindow == null) {
                    _tankkaartToevoegenWindow.lstBestuurder.Items.Clear();
                    _tankkaartToevoegenWindow.lstBestuurder.Items.Add((Bestuurder)lstBestuurder.SelectedItem);
                }
                else if (_voertuigToevoegenWindow == null && _voertuigUpdatenWindow == null && _tankkaartToevoegenWindow == null && _zoekTankkaartWindow == null) {
                    _tankkaartUpdateWindow.lstBestuurder.Items.Clear();
                    _tankkaartUpdateWindow.lstBestuurder.Items.Add((Bestuurder)lstBestuurder.SelectedItem);
                }
                else {
                    _zoekTankkaartWindow.lstBestuurder.Items.Clear();
                    _zoekTankkaartWindow.lstBestuurder.Items.Add((Bestuurder)lstBestuurder.SelectedItem);
                }
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            try {
                lstBestuurder.ItemsSource = _bestuurderManager.GeefBestuurders(txtFamilienaam.Text, txtVoornaam.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
