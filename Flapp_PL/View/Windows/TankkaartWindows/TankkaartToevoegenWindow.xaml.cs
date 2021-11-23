using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for TankkaartToevoegenWindow.xaml
    /// </summary>
    public partial class TankkaartToevoegenWindow : Window {
        private TankkaartManager _tankkaartManager;

        public TankkaartToevoegenWindow() {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(ConfigurationManager.ConnectionStrings["connStringTL"].ConnectionString));
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e) {
            try {
                if (dpGeldigheidsdatum.SelectedDate != null && !string.IsNullOrWhiteSpace(txtPincode.Text)) {
                    Tankkaart t = new Tankkaart(txtPincode.Text, Convert.ToDateTime(dpGeldigheidsdatum.SelectedDate));
                    //_tankkaartManager.VoegTankkaartToe(t);
                    MessageBox.Show("Tankkaart toegevoegd!");
                }
                else { MessageBox.Show("Velden zijn leeg!"); }
                
            }
            catch (Exception) { throw new Exception(); }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
