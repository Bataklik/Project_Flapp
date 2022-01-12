using Flapp_BLL.Managers;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
namespace Flapp_PL {
    public partial class MainWindow : Window {
        ConnectionManager cm;
        public MainWindow() {
            Application.Current.Properties["User"] = ConfigurationManager.ConnectionStrings["connStringA"].ConnectionString;
            //cm = new ConnectionManager(new ConnectionRepo((string)Application.Current.Properties["User"]));
            InitializeComponent();
            //cbUsers.ItemsSource = new List<string> { "Raf", "TiboD", "TiboL", "Burak", "Azure" };
            //var isConnected = false;
            //try { isConnected = cm.IsServerConnected(); }
            //catch (Exception ex) { MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Information); }
            //toggleButtons(isConnected);
        }

        private void btnBestuurder_Click(object sender, RoutedEventArgs e) {
            MainWindow main = this;
            wpUserControl.Children.Clear();
            BestuurderUC bestuurderUc = new BestuurderUC(main);
            wpUserControl.Children.Add(bestuurderUc);
            main.Width = 750;
        }
        private void btnVoertuig_Click(object sender, RoutedEventArgs e) {
            MainWindow main = this;

            wpUserControl.Children.Clear();
            VoertuigUC voertuigUc = new VoertuigUC(main);
            wpUserControl.Children.Add(voertuigUc);
        }
        private void btnTankkaart_Click(object sender, RoutedEventArgs e) {
            MainWindow main = this;
            wpUserControl.Children.Clear();
            TankkaartUC tankkaartUc = new TankkaartUC(main);
            wpUserControl.Children.Add(tankkaartUc);
        }

        private void toggleButtons(bool value) {
            btnBestuurder.IsEnabled = value;
            btnVoertuig.IsEnabled = value;
            btnTankkaart.IsEnabled = value;
            wpUserControl.Children.Clear();
        }

        //private void btnKies_Click(object sender, RoutedEventArgs e) {
        //    switch (cbUsers.SelectedItem) {
        //        case "Raf":
        //            Application.Current.Properties["User"] = ConfigurationManager.ConnectionStrings["connStringR"].ConnectionString;
        //            break;
        //        case "TiboD":
        //            Application.Current.Properties["User"] = ConfigurationManager.ConnectionStrings["connStringTD"].ConnectionString;
        //            break;
        //        case "TiboL":
        //            Application.Current.Properties["User"] = ConfigurationManager.ConnectionStrings["connStringTL"].ConnectionString;
        //            break;
        //        case "Burak":
        //            Application.Current.Properties["User"] = ConfigurationManager.ConnectionStrings["connStringB"].ConnectionString;
        //            break;
        //        case "Azure":
        //            Application.Current.Properties["User"] = ConfigurationManager.ConnectionStrings["connStringA"].ConnectionString;
        //            break;
        //    }
        //    try {
        //        cm = new ConnectionManager(new ConnectionRepo((string)Application.Current.Properties["User"]));
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message); }

        //    toggleButtons(cm.IsServerConnected());
        //    if (!cm.IsServerConnected()) {
        //        btnBestuurder.IsEnabled = false;
        //        btnVoertuig.IsEnabled = false;
        //        btnTankkaart.IsEnabled = false;
        //        wpUserControl.Children.Clear();
        //        MessageBox.Show("Connectie niet gelukt!", "Connection Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    else {
        //        btnBestuurder.IsEnabled = true;
        //        btnVoertuig.IsEnabled = true;
        //        btnTankkaart.IsEnabled = true;
        //        MessageBox.Show("Connectie gekozen!", "Connection Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //}
    }
}
