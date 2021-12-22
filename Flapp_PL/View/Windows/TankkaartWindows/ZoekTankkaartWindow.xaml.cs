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

namespace Flapp_PL.View.Windows.TankkaartWindows
{
    /// <summary>
    /// Interaction logic for ZoekTankkaartWindow.xaml
    /// </summary>
    public partial class ZoekTankkaartWindow : Window
    {
        private MainWindow _main;
        private TankkaartUC _tUC;
        private TankkaartManager _tankkaartManager;

        public ZoekTankkaartWindow(MainWindow main, TankkaartUC tUC)
        {
            _main = main;
            _tUC = tUC;
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            InitializeComponent();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            //if (txtKaartnummer.Text != null && dpGeldigheidsdatum.SelectedDate == null) {
            //    _tUC.lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaartenOpKaartnummer(Int32.Parse(txtKaartnummer.Text)).Select(x => x.Value).ToList();
            //    Close();
            //} else if (txtKaartnummer.Text == null && dpGeldigheidsdatum.SelectedDate != null) {
            //    _tUC.lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaartenOpGeldigheidsdatum((DateTime)dpGeldigheidsdatum.SelectedDate).Select(x => x.Value).ToList();
            //    Close();
            //} 
            try
            { _tUC.lstTankkaarten.ItemsSource = _tankkaartManager.GeefAlleTankkaarten(Int32.Parse(txtKaartnummer.Text), (DateTime?)dpGeldigheidsdatum.SelectedDate); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
