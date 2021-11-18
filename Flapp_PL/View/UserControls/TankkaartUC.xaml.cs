using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public TankkaartUC()
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(ConfigurationManager.ConnectionStrings["connStringTD"].ConnectionString));
            laadTankkaarten();
        }

        private void laadTankkaarten()
        {
            List<Tankkaart> tankkaarten = new List<Tankkaart>();
            try {
                List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().OrderBy(x => x.Value.Kaartnummer).Select(x => x.Value).ToList();
                foreach (Tankkaart v in sortTankkaarten) {
                    tankkaarten.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstTankkaarten.ItemsSource = tankkaarten;
        }

        private void btnZoek_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
