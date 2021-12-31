using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.TankkaartWindows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls {
    public partial class TankkaartUC : UserControl {
        private TankkaartManager _tankkaartManager;
        private MainWindow _main;

        public TankkaartUC() {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            laadTankkaarten();
        }

        public TankkaartUC(MainWindow main) {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _main = main;
            laadTankkaarten();
        }

        private void laadTankkaarten() {
            try {
                lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaarten().Values;
            }
            catch (Exception) { throw; }
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            TankkaartUC tUC = this;
            if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
            new ZoekTankkaartWindow(_main, tUC).ShowDialog();
        }

        private void btnVoegToe_Click(object sender, RoutedEventArgs e) {
            TankkaartUC tUC = this;
            if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
            new TankkaartToevoegenWindow(tUC).ShowDialog();
        }

        private void Update_Click(object sender, RoutedEventArgs e) {
            Tankkaart t = (Tankkaart)lstTankkaarten.SelectedItem;
            TankkaartUC tUC = this;
            new TankkaartUpdateWindow(t, tUC).ShowDialog();
        }

        private void Verwijder_Click(object sender, RoutedEventArgs e) {
            if ((Tankkaart)lstTankkaarten.SelectedItem == null) { MessageBox.Show("U heeft geen tankkaart gekozen!", "Geen tankkaart!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            Tankkaart teVerwijderen = (Tankkaart)lstTankkaarten.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Wilt u zeker Tankkaart: \nKaartnummer: {teVerwijderen.Kaartnummer}, {teVerwijderen.Geldigheidsdatum.ToShortDateString()}\n verwijderen?", "Tankkaart Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.No) { return; }
            try {
                _tankkaartManager.VerwijderTankkaart(teVerwijderen);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaarten().Values;
        }
    }
}
