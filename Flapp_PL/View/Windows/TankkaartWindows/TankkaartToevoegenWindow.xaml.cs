using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Configuration;
using System.Windows;

namespace Flapp_PL.View.Windows.TankkaartWindows
{

    public partial class TankkaartToevoegenWindow : Window
    {
        private TankkaartManager _tankkaartManager;

        public TankkaartToevoegenWindow()
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpGeldigheidsdatum.SelectedDate != null && !string.IsNullOrWhiteSpace(txtPincode.Text))
                {
                    Tankkaart t = new Tankkaart(txtPincode.Text, Convert.ToDateTime(dpGeldigheidsdatum.SelectedDate));
                    //_tankkaartManager.VoegTankkaartToe(t);
                    MessageBox.Show("Tankkaart toegevoegd!");
                }
                else { MessageBox.Show("Velden zijn leeg!"); }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnBestuurder_Click(object sender, RoutedEventArgs e)
        {
            new TankkaartZoekBestuurderWindow().ShowDialog();
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
