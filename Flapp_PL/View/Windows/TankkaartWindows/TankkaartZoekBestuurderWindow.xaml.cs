using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Flapp_PL.View.Windows.TankkaartWindows {
    /// <summary>
    /// Interaction logic for TankkaartZoekBestuurderWindow.xaml
    /// </summary>
    public partial class TankkaartZoekBestuurderWindow : Window {
        private BestuurderManager _bestuurderManager;

        public TankkaartZoekBestuurderWindow() {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadWaarden();
        }

        private void laadWaarden() {
            try {
                lstBestuurders.ItemsSource = _bestuurderManager.GeefAlleBestuurdersZonderTankkaarten();
            }
            catch (Exception) { throw new Exception(); }
        }
                
        private void btnBestuurder_Click(object sender, RoutedEventArgs e) {

        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
