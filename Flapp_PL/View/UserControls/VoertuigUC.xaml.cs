using DocumentFormat.OpenXml.Bibliography;
using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.VoertuigWindow;
using Flapp_PL.View.Windows.VoertuigWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Flapp_PL.View.UserControls
{
    /// <summary>
    /// Interaction logic for VoertuigUC.xaml
    /// </summary>
    public partial class VoertuigUC : UserControl
    {
        #region properties
        private VoertuigManager _voertuigManager;
        private MainWindow _main;
        //private VoertuigZoeken vz = new VoertuigZoeken();
        #endregion
        #region constructors
        public VoertuigUC()
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            laadVoertuigen();
        }
        public VoertuigUC(MainWindow mainw)
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            _main = mainw;
            laadVoertuigen();
        }

        #endregion

        #region methods
        private void laadVoertuigen()
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            try
            {
                foreach (KeyValuePair<int, Voertuig> v in _voertuigManager.GeefVoertuigen())
                {
                    voertuigen.Add(v.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            lstVoertuigen.ItemsSource = voertuigen;
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            //new VoertuigZoeken(_main).ShowDialog();
            VoertuigUC vUC = this;
            if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
            new VoertuigZoeken(_main, vUC).ShowDialog();
        }
        private void btnVoegToe_Click(object sender, RoutedEventArgs e)
        {
            new VoertuigToevoegen().ShowDialog();
        }

        #endregion

        private void itemUpdate_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Voertuig v = (Voertuig)lstVoertuigen.SelectedItem;
            //main.wpUserControl.Children.Add(new VoertuigUpdatenUC(v, main));
            new VoertuigUpdaten(v).ShowDialog();
        }

        private void btnVerwijderVoertuig_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bent u zeker dat u het voertuig wilt verwijdere?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                if ((Voertuig)lstVoertuigen.SelectedItem == null) { MessageBox.Show("U heeft geen voertuig gekozen!", "Geen voertuig!", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                try { _voertuigManager.VerwijderVoertuig((Voertuig)lstVoertuigen.SelectedItem); MessageBox.Show("Verwijderen Gelukt!"); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { laadVoertuigen(); }
            }

        }


    }
}
