using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.VoertuigWindow;
using Flapp_PL.View.Windows.VoertuigWindows;
using System;
using System.Collections.Generic;
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
        private MainWindow main;
        private VoertuigZoeken vz = new VoertuigZoeken();
        #endregion
        #region constructors
        public VoertuigUC()
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));            
            laadVoertuigen();       
        }
        public VoertuigUC(MainWindow main)
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));           
            laadVoertuigen();
        }
        public VoertuigUC(string nplaat, string m, string mo/*, MainWindow main*/)
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));            
            laadVoertuigen(nplaat, m, mo);
        }
        public VoertuigUC(string nplaat)
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            if (vz.cmbMerk.SelectedItem != null) { laadVoertuigenMerk(nplaat); }
            if(vz.txtNummerplaat.Text != null) {laadVoertuigenNplaat(nplaat);}
            
        }
        public VoertuigUC(string nplaat, string m)
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
           
            laadVoertuigen(nplaat, m);
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
        private void laadVoertuigenNplaat(string nplaat)
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            try
            {
                List<Voertuig> sortVoertuig = _voertuigManager.GeefAlleVoertuigen().Where(b => (b.Nummerplaat == nplaat)).ToList();
                foreach (Voertuig v in sortVoertuig)
                {
                    voertuigen.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstVoertuigen.ItemsSource = voertuigen;
        }
        private void laadVoertuigenMerk(string nplaat)
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            try
            {
                List<Voertuig> sortVoertuig = _voertuigManager.GeefAlleVoertuigen().Where(b => (b.Merk == nplaat)).ToList();
                foreach (Voertuig v in sortVoertuig)
                {
                    voertuigen.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstVoertuigen.ItemsSource = voertuigen;
        }
        private void laadVoertuigen(string nplaat, string m)
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            try
            {
                List<Voertuig> sortVoertuig = _voertuigManager.GeefAlleVoertuigen().Where(b => (b.Nummerplaat == nplaat) && (b.Merk == m)).ToList();
                foreach (Voertuig v in sortVoertuig)
                {
                    voertuigen.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstVoertuigen.ItemsSource = voertuigen;
        }
        private void laadVoertuigen(string nplaat, string merk, string model)
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            try
            {
                List<Voertuig> sortVoertuig = _voertuigManager.GeefAlleVoertuigen().Where(b => (b.Nummerplaat == nplaat) && (b.Merk == merk) && (b.Model==model)).ToList();
                foreach (Voertuig v in sortVoertuig)
                {
                    voertuigen.Add(v);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstVoertuigen.ItemsSource = voertuigen;
        }
        private void btnVoertuigToevoegen_Click(object sender, RoutedEventArgs e)
        {
            new VoertuigToevoegen().ShowDialog();            
        }
        private void btnVoertuigZoeken_Click(object sender, RoutedEventArgs e)
        {            
            new VoertuigZoeken(main).ShowDialog();           
        }
        #endregion
    }
}
