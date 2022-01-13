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

        public void laadTankkaarten() {
            try {
                lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaarten().Values;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            try {
                if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
                new ZoekTankkaartWindow(_main, this).ShowDialog();
            }
            catch (Exception) { MessageBox.Show("Er is een fout opgetreden tijdens het zoeken!", "Zoeken fout", MessageBoxButton.OK, MessageBoxImage.Hand); ; }
        }

        private void btnVoegToe_Click(object sender, RoutedEventArgs e) {
            try {
                if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
                new TankkaartToevoegenWindow(this).ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }         
        }

        private void Update_Click(object sender, RoutedEventArgs e) {
            try {
                Tankkaart t = (Tankkaart)lstTankkaarten.SelectedItem;
                new TankkaartUpdateWindow(t, this).ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }       
        }

        private void Verwijder_Click(object sender, RoutedEventArgs e) {
            try {
                if ((Tankkaart)lstTankkaarten.SelectedItem == null) { MessageBox.Show("U heeft geen tankkaart gekozen!", "Geen tankkaart!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                Tankkaart teVerwijderen = (Tankkaart)lstTankkaarten.SelectedItem;
                MessageBoxResult result = MessageBox.Show($"Wilt u zeker Tankkaart: \nKaartnummer: {teVerwijderen.Kaartnummer} verwijderen?", "Tankkaart Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.No) { return; }
                _tankkaartManager.VerwijderTankkaart(teVerwijderen);
                lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaarten().Values;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }       
        }
    }
}
