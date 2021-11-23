using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.TankkaartWindows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls
{
    /// <summary>
    /// Interaction logic for TankkaartUC.xaml
    /// </summary>
    public partial class TankkaartUC : UserControl
    {
        private TankkaartManager _tankkaartManager;
        private MainWindow _main;

        public TankkaartUC()
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(ConfigurationManager.ConnectionStrings["connStringTL"].ConnectionString));
            laadTankkaarten();
        }

        public TankkaartUC(string kaartnummer, DateTime datum)
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(ConfigurationManager.ConnectionStrings["connStringTL"].ConnectionString));
            laadTankkaarten(kaartnummer, datum);
        }

        public TankkaartUC(string kaartnummer)
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(ConfigurationManager.ConnectionStrings["connStringTL"].ConnectionString));
            laadTankkaarten(kaartnummer);
        }

        public TankkaartUC(MainWindow main)
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(ConfigurationManager.ConnectionStrings["connStringTL"].ConnectionString));
            _main = main;
            laadTankkaarten();
        }

        public TankkaartUC(DateTime datum)
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(ConfigurationManager.ConnectionStrings["connStringTL"].ConnectionString));
            laadTankkaarten(datum);
        }

        private void laadTankkaarten()
        {
            List<Tankkaart> tankkaarten = new List<Tankkaart>();
            try
            {
                List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().OrderBy(x => x.Value.Kaartnummer).Select(x => x.Value).ToList();
                foreach (Tankkaart v in sortTankkaarten)
                {
                    tankkaarten.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstTankkaarten.ItemsSource = tankkaarten;
        }

        private void laadTankkaarten(string kaartnummer, DateTime datum)
        {
            List<Tankkaart> tankkaarten = new List<Tankkaart>();
            try
            {
                List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().Where(x => x.Value.Kaartnummer == int.Parse(kaartnummer) || x.Value.Geldigheidsdatum.ToShortDateString() == datum.ToShortDateString()).OrderBy(x => x.Value.Kaartnummer).Select(x => x.Value).ToList();
                foreach (Tankkaart v in sortTankkaarten)
                {
                    tankkaarten.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstTankkaarten.ItemsSource = tankkaarten;
        }

        private void laadTankkaarten(string kaartnummer)
        {
            List<Tankkaart> tankkaarten = new List<Tankkaart>();
            try
            {
                List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().Where(x => x.Value.Kaartnummer == int.Parse(kaartnummer)).OrderBy(x => x.Value.Kaartnummer).Select(x => x.Value).ToList();
                foreach (Tankkaart v in sortTankkaarten)
                {
                    tankkaarten.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstTankkaarten.ItemsSource = tankkaarten;
        }

        private void laadTankkaarten(DateTime datum)
        {
            List<Tankkaart> tankkaarten = new List<Tankkaart>();
            try
            {
                List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().Where(x => x.Value.Geldigheidsdatum.ToShortDateString() == datum.ToShortDateString()).OrderBy(x => x.Value.Kaartnummer).Select(x => x.Value).ToList();
                foreach (Tankkaart v in sortTankkaarten)
                {
                    tankkaarten.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstTankkaarten.ItemsSource = tankkaarten;
        }

        private void btnZoek_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TankkaartUC tUC = this;
            new ZoekTankkaartWindow(_main, tUC).ShowDialog();
        }

        private void btnVoegToe_Click(object sender, RoutedEventArgs e) {
            TankkaartUC tUC = this;
            new TankkaartToevoegenWindow().ShowDialog();
        }
    }
}
