using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
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
    /// Interaction logic for ZoekTankkaartWindow.xaml
    /// </summary>
    public partial class ZoekTankkaartWindow : Window {
        private MainWindow main;
        private TankkaartUC tUC;
        private TankkaartManager tankkaartManager;

        public ZoekTankkaartWindow(MainWindow main, TankkaartUC tUC) {
            this.main = main;
            this.tUC = tUC;
            tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            InitializeComponent();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e) {
            List<Tankkaart> tankkaarten = new List<Tankkaart>();
            //int kaartnummer = Convert.ToInt32(txtKaartnummer.Text);
            if(dpGeldigheidsdatum.SelectedDate == null)
            {
                try {
                    foreach (KeyValuePair<int, Tankkaart> v in tankkaartManager.GeefAlleTankkaarten(Convert.ToInt32(txtKaartnummer.Text), (DateTime)dpGeldigheidsdatum.SelectedDate)) {
                        tankkaarten.Add(v.Value);
                    }
                    tUC.lstTankkaarten.ItemsSource = tankkaarten;
                }
                catch (Exception) { throw; }
            }
            
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
