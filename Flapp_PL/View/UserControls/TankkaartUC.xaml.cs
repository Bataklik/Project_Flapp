using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls
{
    /// <summary>
    /// Interaction logic for TankkaartUC.xaml
    /// </summary>
    public partial class TankkaartUC : UserControl
    {
        private TankkaartManager _tankkaartManager;
        private string _connStringBurak = @"Data Source=LAPTOP-BURAQ\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";
        private string _connStringTiboDesktop = @"Data Source=DESKTOP-8JVOTB1\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";
        private string _connStringTiboLaptop = @"Data Source=LAPTOP-GTB3LMSV\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";
        private string _connStringRaf = @"Data Source=LAPTOP-4QVTNHR0\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";

        public TankkaartUC()
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(_connStringTiboLaptop));
        }

        private void laadTankkaarten() {
            List<Tankkaart> tankkaarten = new List<Tankkaart>();
            try {
                //List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().Where(b => (b.Kaartnummer == kaartnummer) && (b.Value.Voornaam == voornaam) && (b.Value.Geboortedatum.ToShortDateString() == datum.ToShortDateString())).Select(b => b.Value).ToList();
                foreach (Tankkaart v in tankkaarten) {
                    tankkaarten.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstTankkaarten.ItemsSource = tankkaarten;
        }

        private void btnZoek_Click(object sender, System.Windows.RoutedEventArgs e) {

        }
    }
}
