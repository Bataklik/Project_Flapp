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
                List<Voertuig> sortVoertuigen = new List<Voertuig>(_voertuigManager.GeefAlleVoertuigen());
                foreach (Voertuig v in sortVoertuigen)
                {
                    voertuigen.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstVoertuigen.ItemsSource = voertuigen;
        }       
        private void laadVoertuigen(string nummerplaat, string merk, string model)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(nummerplaat))
                {
                    //nummerplaat = txtNummerplaat.Text;
                }
                if (!string.IsNullOrWhiteSpace(merk))
                {
                    //merk = cmbMerk.SelectedItem.ToString();
                }
                if (!string.IsNullOrWhiteSpace(model))
                {
                    //model = null;
                    //model = cmbModel.SelectedItem.ToString();
                }
                //List<Voertuig> voertuigen = _voertuigmanager.VoertuigZoeken(nummerplaat, merk, model);
                List<Voertuig> voertuigen = _voertuigManager.VoertuigZoeken(nummerplaat, merk, model);
                ObservableCollection<Voertuig> ts = new();
                foreach (Voertuig bestelling in voertuigen)
                {
                    ts.Add(bestelling);
                }
                lstVoertuigen.ItemsSource = ts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
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
            _main = new MainWindow();
            Voertuig v = (Voertuig)lstVoertuigen.SelectedItem;
            _main.wpUserControl.Children.Add(new VoertuigUpdatenUC(v, _main));
        }
    }
}
