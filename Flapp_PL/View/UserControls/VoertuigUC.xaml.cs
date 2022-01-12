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
        public void laadVoertuigen()
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
            VoertuigUC vUC = this;
            if (_main.wpUserControl.Children.Count > 1) _main.wpUserControl.Children.RemoveAt(_main.wpUserControl.Children.Count - 1);
            new VoertuigZoeken(_main, vUC).ShowDialog();
        }
        private void btnVoegToe_Click(object sender, RoutedEventArgs e)
        {
            new VoertuigToevoegen(this).ShowDialog();
        }

        #endregion

        private void itemUpdate_Click(object sender, RoutedEventArgs e)
        {
            new VoertuigUpdaten((Voertuig)lstVoertuigen.SelectedItem).ShowDialog();
        }

        private void btnVerwijderVoertuig_Click(object sender, RoutedEventArgs e)
        {
            if ((Voertuig)lstVoertuigen.SelectedItem == null) { MessageBox.Show("U heeft geen voertuig gekozen!", "Geen voertuig!", MessageBoxButton.OK, MessageBoxImage.Hand); return; }
            Voertuig teVerwijderen = (Voertuig)lstVoertuigen.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Wilt u zeker Voertuig: \n{teVerwijderen.Merk} {teVerwijderen.Model}, {teVerwijderen.Nummerplaat}\n verwijderen?", "Voertuig Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.No) { return; }

            try { _voertuigManager.VerwijderVoertuig((Voertuig)lstVoertuigen.SelectedItem); }
            catch (Exception ex) {MessageBox.Show(ex.Message); }
            finally { laadVoertuigen(); }
        }

    }
}
