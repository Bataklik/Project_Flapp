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
        private MainWindow main;
        private VoertuigManager _voertuigmanager;
        private BrandstofManager _brandstofManager;        
        public VoertuigZoeken(MainWindow main)
        {
            this.main = main;
                     
            _voertuigmanager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            InitializeComponent();   
            laadBrandstoffen();
            laadMerk();
            
        }
        public void laadBrandstoffen()
        {
            try { lstBrandstoftype.ItemsSource = (List<Brandstof>)_brandstofManager.GeefAlleBrandstoffen(); }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }
        
        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {            
            //main = new();
            //main.wpUserControl.Children.Clear();
            //main.wpUserControl.Children.Add(new VoertuigUC(txtNummerplaat.Text, cmbMerk.SelectedItem.ToString(), cmbModel.SelectedItem.ToString(), main));
            //Close();
            //return;
            
                //if (!string.IsNullOrWhiteSpace(txtNummerplaat.Text))
                //{
                //    nummerplaat = txtNummerplaat.Text;
                //}
                //if (cmbMerk.SelectedIndex != 0)
                //{
                //    merk = cmbMerk.SelectedItem.ToString();
                //}
                //if (cmbModel.SelectedIndex != 0)
                //{
                //    model = null;
                //    model = cmbModel.SelectedItem.ToString();
                //}
                //List<Voertuig> voertuigen = _voertuigmanager.VoertuigZoeken(nummerplaat, merk, model);
                List<Voertuig> voertuigen = new List<Voertuig>();
                try
                {
                    foreach (KeyValuePair<int, Voertuig> vo in _voertuigmanager.SearchVehicle(cmbMerk.Text, cmbModel.Text, txtNummerplaat.Text))
                    {
                        voertuigen.Add(vo.Value);
                    }
                    lstVoertuigen.ItemsSource = voertuigen;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error); }          
           
        }
        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        void laadMerk()
        {
            ObservableCollection<string> merken = new(_voertuigmanager.geefMerken());
            merken.Insert(0, "");
            //cmbMerk.SelectedIndex = 0;
            cmbMerk.ItemsSource = merken;
        }
        private void cmbMerk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMerk.SelectedIndex != 0)
            {
                //cmbModel.IsEnabled = true;
                ObservableCollection<string> modellen = new(_voertuigmanager.geefModellen(cmbMerk.SelectedItem.ToString()));
                modellen.Insert(0, "");
                cmbModel.ItemsSource = modellen;
                cmbModel.SelectedIndex = 0;
                //merk = cmbMerk.SelectedItem.ToString();
                //model = null;
            }
            else
            {
                //cmbModel.IsEnabled = false;
                //cmbModel.ItemsSource = null;
            }
        }
        private void UpdateVoertuig_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 
                Voertuig v = (Voertuig)lstVoertuigen.SelectedItem;
                new VoertuigUpdaten(v.VoertuigID).ShowDialog();
                //NavigationService.Navigate(new Uri("/Pages/Klant/KlantUpdatenPage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DeleteVoertuig_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
