using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Flapp_PL.View.Windows.VoertuigWindows;

namespace Flapp_PL.View.Windows.VoertuigWindow
{
    public partial class VoertuigToevoegen : Window
    {
        private VoertuigManager _voertuigManager;
        private ObservableCollection<Brandstof> _brandstoffen;
        private ObservableCollection<Bestuurder> _bestuurders;
        private BrandstofManager _brandstofmanager;
        private VoertuigTypeManager _voertuigTypeManager;
        private BestuurderManager _bestuurderManager;
        private int aantalDeuren;
        public VoertuigToevoegen()
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            _voertuigTypeManager = new VoertuigTypeManager(new VoertuigTypeRepo(Application.Current.Properties["User"].ToString()));
            _brandstofmanager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            if (Application.Current.Properties["Brandstof"] == null)
            {
                Application.Current.Properties["Brandstof"] = new ObservableCollection<Brandstof>();
                _brandstoffen = (ObservableCollection<Brandstof>)Application.Current.Properties["Brandstof"]; ;
            }
            else { _brandstoffen = (ObservableCollection<Brandstof>)Application.Current.Properties["Brandstof"]; ; }

            if (Application.Current.Properties["Bestuurder"] == null)
            {
                Application.Current.Properties["Bestuurder"] = new ObservableCollection<Bestuurder>();
                _bestuurders = (ObservableCollection<Bestuurder>)Application.Current.Properties["Bestuurder"]; ;
            }
            else { _bestuurders = (ObservableCollection<Bestuurder>)Application.Current.Properties["Bestuurder"]; ; }

            //laadVoertuigtypes();
            //laadBrandstoftypes();
            laadMerk();
        }
        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                Voertuig voertuig = null;
                voertuig = new Voertuig(cmbMerk.Text.ToUpper(), cmbModel.Text.ToUpper(), txtChassis.Text.ToUpper(), txtNummerplaat.Text.ToUpper(), lstBrandtof.Items.Cast<Brandstof>().ToList(), cmbType.SelectedItem.ToString().ToUpper(), txtKleur.Text.ToUpper(), Convert.ToInt32(txtDeuren.Text));
                voertuig.ZetVoeruigID(_voertuigManager.VoegVoertuigToe(voertuig));
                _brandstofmanager.VoegBrandstofToeAanVoertuig(voertuig.VoertuigID, voertuig.Brandstof);                 
                
                MessageBox.Show("Voertuig is toegevoegd!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }        
        private void VerwijderBrandstof_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Brandstof x = (Brandstof)lstBrandtof.SelectedItem;
                int brandstof = x.Id;
                _brandstoffen.Remove(x);
                Application.Current.Properties["Brandstof"] = _brandstoffen;
                MessageBox.Show("Brandstof is verwijderd van de wagen", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }          
        private void laadMerk()
        {
            ObservableCollection<string> merken = new(_voertuigManager.geefMerken());
            merken.Insert(0, "");
            //cmbMerk.SelectedIndex = 0;
            cmbMerk.ItemsSource = merken;
        }             
        private void laadVoertuigtypes()
        {
            
        }
        private void cmbMerk_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(cmbMerk.SelectedIndex == 0)
            {
                //cmbModel.IsEnabled = false;
                cmbModel.ItemsSource = null;
            }
            else
            {
                //cmbModel.IsEnabled = true;
                ObservableCollection<string> modellen = new(_voertuigManager.geefModellen(cmbMerk.SelectedItem.ToString()));
                modellen.Insert(0, "");
                cmbModel.ItemsSource = modellen;
                cmbModel.SelectedIndex = 0;
                //merk = cmbMerk.SelectedItem.ToString();
                //model = null;
            }            
        }
        private void cmbBrandstoffen_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbBrandstoffen.ItemsSource = _brandstofmanager.GeefAlleBrandstoffen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnAddBrandstof_Click(object sender, RoutedEventArgs e)
        {
            if ((Brandstof)cmbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (lstBrandtof.Items.Contains((Brandstof)cmbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
            lstBrandtof.Items.Add((Brandstof)cmbBrandstoffen.SelectedItem);
        }
        private void btnRemoveBrandstof_Click(object sender, RoutedEventArgs e)
        {
            if ((Brandstof)lstBrandtof.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (!lstBrandtof.Items.Contains((Brandstof)lstBrandtof.SelectedItem)) { MessageBox.Show("Brandstof staat niet op de lijst!"); return; }
            lstBrandtof.Items.Remove((Brandstof)cmbBrandstoffen.SelectedItem);
        }
        private void txtDeuren_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void cmbType_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                IReadOnlyList<string> types = _voertuigTypeManager.GeefAlleVoertuigTypes();
                ObservableCollection<string> ts = new();
                foreach (var type in types)
                {
                    ts.Add(type);
                }
                cmbType.ItemsSource = ts;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }

        private void btnPlusDeur_Click(object sender, RoutedEventArgs e)
        {
            aantalDeuren = Convert.ToInt32(txtDeuren.Text);
            aantalDeuren += 1;
            txtDeuren.Text = aantalDeuren.ToString();
        }

        private void btnMindeur_Click(object sender, RoutedEventArgs e)
        {
            aantalDeuren = Convert.ToInt32(txtDeuren.Text);
            aantalDeuren -= 1;
            txtDeuren.Text = aantalDeuren.ToString();
        }
    }
}
