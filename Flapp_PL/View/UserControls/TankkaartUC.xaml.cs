using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls.TankkaartUCs;
using Flapp_PL.View.Windows.TankkaartWindows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls
{
    public partial class TankkaartUC : UserControl
    {
        private TankkaartManager _tankkaartManager;
        private MainWindow _main;

        public TankkaartUC()
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            laadTankkaarten();
        }

        public TankkaartUC(string kaartnummer, DateTime datum, MainWindow main)
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _main = main;
            laadTankkaarten(kaartnummer, datum);
        }

        public TankkaartUC(string kaartnummer, MainWindow main)
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _main = main;
            laadTankkaarten(kaartnummer);
        }

        public TankkaartUC(DateTime datum, MainWindow main) {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _main = main;
            laadTankkaarten(datum);
        }

        public TankkaartUC(MainWindow main)
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _main = main;
            laadTankkaarten();
        }

        private void laadTankkaarten()
        {
            List<Tankkaart> tankkaarten = new List<Tankkaart>();
            try
            {
                List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().OrderBy(x => x.Kaartnummer).ToList();
                foreach (Tankkaart v in sortTankkaarten)
                { tankkaarten.Add(v); }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstTankkaarten.ItemsSource = tankkaarten;
        }

        private void laadTankkaarten(string kaartnummer, DateTime datum)
        {
            List<Tankkaart> tankkaarten = new List<Tankkaart>();
            try
            {
                List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().Where(x => x.Kaartnummer == int.Parse(kaartnummer) || x.Geldigheidsdatum.ToShortDateString() == datum.ToShortDateString()).OrderBy(x => x.Kaartnummer).ToList();
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
                List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().Where(x => x.Kaartnummer == int.Parse(kaartnummer)).OrderBy(x => x.Kaartnummer).ToList();
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
                List<Tankkaart> sortTankkaarten = _tankkaartManager.GeefAlleTankkaarten().Where(x => x.Geldigheidsdatum.ToShortDateString() == datum.ToShortDateString()).OrderBy(x => x.Kaartnummer).ToList();
                foreach (Tankkaart v in sortTankkaarten)
                {
                    tankkaarten.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstTankkaarten.ItemsSource = tankkaarten;
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
            new ZoekTankkaartWindow(_main).ShowDialog();
        }

        private void btnVoegToe_Click(object sender, RoutedEventArgs e)
        {
            if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
            new TankkaartToevoegenWindow().ShowDialog();
        }

        private void UpdateTankkaart_Click(object sender, RoutedEventArgs e)
        {

            Tankkaart t = (Tankkaart)lstTankkaarten.SelectedItem;
            TankkaartUC tUC = this;
            _main.wpUserControl.Children.Add(new TankkaartUpdateUC(t, _main, tUC));
        }
    }
}
