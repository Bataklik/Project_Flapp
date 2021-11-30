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
        private string si;
        public VoertuigZoeken()
        {
            InitializeComponent();
        }
        public VoertuigZoeken(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            _voertuigmanager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
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
            main = new MainWindow();
            if (cmbMerk.SelectedValue.ToString() != "<geen merk>") {
                main.Show();
                main.wpUserControl.Children.Clear();
                main.wpUserControl.Children.Add(new VoertuigUC(cmbMerk.SelectedValue.ToString())); ;
                Close();
                return;
            } if (!string.IsNullOrWhiteSpace(txtNummerplaat.Text)) { //<-- ALLEEN DEZE WERKT
                main.Show();
                main.wpUserControl.Children.Clear();
                main.wpUserControl.Children.Add(new VoertuigUC(txtNummerplaat.Text.ToUpper()));
                Close();
                return;
            } else if (cmbMerk.SelectedItem != null && cmbModel.SelectedItem != null) {
                main.Show();
                main.wpUserControl.Children.Clear();
                main.wpUserControl.Children.Add(new VoertuigUC(cmbMerk.SelectedItem.ToString(), cmbModel.SelectedItem.ToString()));
                Close();
                return;
            } else if (!string.IsNullOrWhiteSpace(txtNummerplaat.Text) && cmbMerk.SelectedItem != null) {
                main.Show();
                main.wpUserControl.Children.Clear();
                main.wpUserControl.Children.Add(new VoertuigUC(txtNummerplaat.Text, cmbMerk.SelectedItem.ToString()));
                Close();
                return;
            } /*else*/if (!string.IsNullOrWhiteSpace(txtNummerplaat.Text) && cmbMerk.SelectedItem != null && cmbModel.SelectedItem != null) {
                main.Show();
                main.wpUserControl.Children.Clear();
                main.wpUserControl.Children.Add(new VoertuigUC(txtNummerplaat.Text.ToUpper(), cmbMerk.SelectedItem.ToString(), cmbModel.SelectedItem.ToString()/*, main*/));
                Close();
                return;
            }
            MessageBox.Show("Velden zijn leeg!");
        }
        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        void laadMerk()
        {
            ObservableCollection<string> merken = new(_voertuigmanager.geefMerken());
            merken.Insert(0, "<geen merk>");
            cmbMerk.SelectedIndex = 0;
            cmbMerk.ItemsSource = merken;
        }
        private void cmbMerk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMerk.SelectedIndex != 0)
            {
                cmbModel.IsEnabled = true;
                ObservableCollection<string> modellen = new(_voertuigmanager.geefModellen(cmbMerk.SelectedItem.ToString()));
                modellen.Insert(0, "<geen model>");
                cmbModel.ItemsSource = modellen;
                cmbModel.SelectedIndex = 0;
                si = cmbMerk.SelectedItem.ToString();
            }
            else
            {
                cmbModel.IsEnabled = false;
                cmbModel.ItemsSource = null;
            }
        }
    }
}
