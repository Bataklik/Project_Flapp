using DocumentFormat.OpenXml.Bibliography;
using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls.VoertuigUCs;
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
        public VoertuigUC(string nummerplaat, string merk, string model, MainWindow mainw)
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            _main = mainw;
            laadVoertuigen(nummerplaat, merk, model);
        }
        
        #endregion

        #region methods
        private void laadVoertuigen()
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            try
            {                
                foreach (KeyValuePair<int,Voertuig> v in _voertuigManager.GeefAlleVoertuigen())
                {
                    voertuigen.Add(v.Value);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstVoertuigen.ItemsSource = voertuigen;
        }       
        private void laadVoertuigen(string nummerplaat, string merk, string model)
        {
            
        }
        private void btnVoertuigToevoegen_Click(object sender, RoutedEventArgs e)
        {
            new VoertuigToevoegen().ShowDialog();            
        }
        private void btnVoertuigZoeken_Click(object sender, RoutedEventArgs e)
        {            
            new VoertuigZoeken(_main).ShowDialog();           
        }
        #endregion

        private void itemUpdate_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Voertuig v = (Voertuig)lstVoertuigen.SelectedItem;
            //main.wpUserControl.Children.Add(new VoertuigUpdatenUC(v, main));
            new VoertuigUpdaten(v.VoertuigID).ShowDialog();
        }
    }
}
