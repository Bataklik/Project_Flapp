using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls
{
    /// <summary>
    /// Interaction logic for BestuurderUC.xaml
    /// </summary>
    public partial class BestuurderUC : UserControl
    {
        private BestuurderManager _bestuurderManager;
        private string _connStringBurak = @"Data Source=LAPTOP-BURAQ\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";
        private string _connStringTiboDesktop = @"Data Source=DESKTOP-8JVOTB1\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";
        private string _connStringRaf = @"Data Source=LAPTOP-4QVTNHR0\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";
        public BestuurderUC()
        {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(_connStringRaf));

            laadBestuurders();
        }

        private void laadBestuurders()
        {
            IReadOnlyList<Bestuurder> bestuurders = null;
            try
            {
                bestuurders = _bestuurderManager.GeefAlleBestuurders();
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstbBestuurders.ItemsSource = bestuurders;
        }
    }
}
