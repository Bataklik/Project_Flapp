using Flapp_BLL.Managers;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using System;
using System.Linq;
using System.Windows;

namespace Flapp_PL.View.Windows.TankkaartWindows {
    /// <summary>
    /// Interaction logic for ZoekTankkaartWindow.xaml
    /// </summary>
    public partial class ZoekTankkaartWindow : Window {
        private MainWindow _main;
        private TankkaartUC _tUC;
        private TankkaartManager _tankkaartManager;

        public ZoekTankkaartWindow(MainWindow main, TankkaartUC tUC) {
            _main = main;
            _tUC = tUC;
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            InitializeComponent();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            try {
                if (txtKaartnummer.Text != null && dpGeldigheidsdatum.SelectedDate == null) {
                    _tUC.lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaartenOpKaartnummer(Int32.Parse(txtKaartnummer.Text)).Select(x => x.Value).ToList();
                    Close();
                }
                if (dpGeldigheidsdatum != null && string.IsNullOrWhiteSpace(txtKaartnummer.Text)) {
                    _tUC.lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaartenOpGeldigheidsdatum((DateTime)dpGeldigheidsdatum.SelectedDate).Select(x => x.Value).ToList();
                    Close();
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
