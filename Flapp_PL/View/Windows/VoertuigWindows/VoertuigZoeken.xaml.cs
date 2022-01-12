using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Flapp_PL.View.Windows.VoertuigWindows
{
    /// <summary>
    /// Interaction logic for VoertuigZoeken.xaml
    /// </summary>
    public partial class VoertuigZoeken : Window
    {
        private MainWindow ain;
        private VoertuigManager _voertuigmanager;
        private BrandstofManager _brandstofManager;
        private VoertuigUC vUC;
        public VoertuigZoeken(MainWindow main, VoertuigUC vUC)
        {
            ain = main;
            this.vUC = vUC;
            _voertuigmanager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            InitializeComponent();                     
        }
       
        
        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {            
            List<Voertuig> voertuigen = new List<Voertuig>();
            try
            {
                foreach (KeyValuePair<int, Voertuig> vo in _voertuigmanager.ZoekVoertuig(cmbMerk.Text, cmbModel.Text, txtNummerplaat.Text))
                {
                    voertuigen.Add(vo.Value);
                }
                vUC.lstVoertuigen.ItemsSource = voertuigen;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }                     
        }
        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void cmbMerk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbMerk.SelectedIndex > -1)
                {
                    ObservableCollection<string> modellen = new(_voertuigmanager.GeefModellenMerk(cmbMerk.SelectedItem.ToString()));
                    cmbModel.ItemsSource = modellen;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void cmbMerk_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<string> merken = new(_voertuigmanager.GeefMerken());
                cmbMerk.ItemsSource = merken;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }            
        }
    }
}
