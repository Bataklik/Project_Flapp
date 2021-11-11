using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls
{
    public partial class BestuurderUC : UserControl
    {
        private BestuurderManager _bestuurderManager;
        private string _connStringBurak = @"Data Source=LAPTOP-BURAQ\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";
        private string _connStringTiboDesktop = @"Data Source=DESKTOP-8JVOTB1\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";
        private string _connStringRaf = @"Data Source=LAPTOP-4QVTNHR0\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";

        public BestuurderUC()
        {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(_connStringBurak));

            laadBestuurders();
        }

        public BestuurderUC(string naam, string voornaam, DateTime datum)
        {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(_connStringBurak));

            laadBestuurders(naam, voornaam, datum);
        }

        private void laadBestuurders()
        {
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            try
            {
                foreach (KeyValuePair<int, Bestuurder> v in _bestuurderManager.GeefAlleBestuurders(10))
                {
                    bestuurders.Add(v.Value);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstbBestuurders.ItemsSource = bestuurders;
        }
        private void laadBestuurders(string naam, string voornaam, DateTime datum)
        {
            List<Bestuurder> bestuurders = new List<Bestuurder>();
            try
            {
                List<Bestuurder> sortBestuurder = _bestuurderManager.GeefAlleBestuurders().Where(b => (b.Value.Naam == naam) && (b.Value.Voornaam == voornaam) && (b.Value.Geboortedatum.ToShortDateString() == datum.ToShortDateString())).Select(b => b.Value).ToList();
                foreach (Bestuurder v in sortBestuurder)
                {
                    bestuurders.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstbBestuurders.ItemsSource = bestuurders;
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            Window myWindow = Window.GetWindow(this);
            new ZoekBestuurderWindow().Show();

            myWindow.Close();
        }
    }
}
