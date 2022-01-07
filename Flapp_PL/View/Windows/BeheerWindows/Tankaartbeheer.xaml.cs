using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.BestuurderWindows;
using Flapp_PL.View.Windows.TankkaartWindows;
using System;
using System.Windows;

namespace Flapp_PL.View.Windows.BeheerWindows {
    public partial class Tankaartbeheer : Window {
        private VoegBestuurderToe _parentWindow;
        private UpdateBestuurderWindow _parentUpdateWindow;

        private TankkaartManager _tankkaartManager;
        public Tankaartbeheer(VoegBestuurderToe parentWindow) {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _parentWindow = parentWindow;
            LaadTankkaarten();
        }
        public Tankaartbeheer(UpdateBestuurderWindow parentWindow) {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _parentUpdateWindow = parentWindow;
            LaadTankkaarten();
        }

        #region Click Methods
        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            if ((startDp.SelectedDate == null && endDp.SelectedDate != null) || (startDp.SelectedDate != null && endDp.SelectedDate == null)) { MessageBox.Show("Geen datum geselecteerd!", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            if (startDp.SelectedDate == null && endDp.SelectedDate == null) { lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaartenZonderBestuurder(null, null).Values; }
            if (startDp.SelectedDate != null && endDp.SelectedDate != null) {
                lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaartenZonderBestuurder(startDp.SelectedDate, endDp.SelectedDate).Values;
            
            }

        }
        private void miSelecteer_Click(object sender, RoutedEventArgs e) {
            if (lstTankkaarten.SelectedItems == null) { MessageBox.Show("U heeft geen tankkaart geselecteerd!"); return; }
            if (_parentWindow == null) {
                _parentUpdateWindow.lstTankkaart.Items.Clear();
                _parentUpdateWindow.lstTankkaart.Items.Add((Tankkaart)lstTankkaarten.SelectedItem);
            }
            else {
                _parentWindow.lstTankkaart.Items.Clear();
                _parentWindow.lstTankkaart.Items.Add((Tankkaart)lstTankkaarten.SelectedItem);
            }
            Close();
        }
        private void miVoegToe_Click(object sender, RoutedEventArgs e) {
            new TankkaartToevoegenWindow(this).ShowDialog();
        }
        private void miVerwijderen_Click(object sender, RoutedEventArgs e) {
            if (lstTankkaarten.SelectedItem == null) { MessageBox.Show("U heeft geen tankkaart geselecteerd"); }
            try {
                _tankkaartManager.VerwijderTankkaart((Tankkaart)lstTankkaarten.SelectedItem);
                LaadTankkaarten();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion

        #region Helpers
        public void LaadTankkaarten() {
            try {
                lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaartenZonderBestuurder(null, null).Values;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion
    }
}
