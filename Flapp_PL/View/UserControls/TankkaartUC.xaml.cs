using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls.TankkaartUCs;
using Flapp_PL.View.Windows.TankkaartWindows;
using System;
using System.Collections.ObjectModel;
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
                ObservableCollection<Tankkaart> oc = new();
                foreach (var v in _tankkaartManager.GeefAlleTankkaarten().Select(x => x.Value)) {
                    oc.Add(v);
                }
                lstTankkaarten.ItemsSource = oc;
            }
            catch (Exception) { throw; }
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            TankkaartUC tUC = this;
            if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
            new ZoekTankkaartWindow(_main, tUC).ShowDialog();
        }

        private void btnVoegToe_Click(object sender, RoutedEventArgs e) {
            if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
            new TankkaartToevoegenWindow().ShowDialog();
        }

        private void UpdateTankkaart_Click(object sender, RoutedEventArgs e) {
            Tankkaart t = (Tankkaart)lstTankkaarten.SelectedItem;
            TankkaartUC tUC = this;
            _main.wpUserControl.Children.Add(new TankkaartUpdateUC(t, _main, tUC));
        }
    }
}
