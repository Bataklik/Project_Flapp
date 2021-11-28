using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Flapp_PL.View.Windows.VoertuigWindow
{
    public partial class VoertuigToevoegen : Window
    {
        private VoertuigManager _voertuigManager;
        private ObservableCollection<Brandstof> _brandstoffen;
        private VoertuigTypeManager _voertuigTypeManager;

        public VoertuigToevoegen()
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            _voertuigTypeManager = new VoertuigTypeManager(new VoertuigTypeRepo(Application.Current.Properties["User"].ToString()));
            if (Application.Current.Properties["Brandstof"] == null)
            {
                Application.Current.Properties["Brandstof"] = new ObservableCollection<Brandstof>();
                _brandstoffen = (ObservableCollection<Brandstof>)Application.Current.Properties["Brandstof"]; ;
            }
            else { _brandstoffen = (ObservableCollection<Brandstof>)Application.Current.Properties["Brandstof"]; ; }
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            VoertuigType t = new VoertuigType(cmbType.SelectedIndex.ToString());
            Voertuig v = new Voertuig(txtMerk.Text, txtModel.Text, txtChassis.Text, txtNummerplaat.Text, t, txtKleur.Text, Convert.ToInt32(txtDeuren.Text));
            try
            {
                _voertuigManager.VoegVoertuigToe(v);
                MessageBox.Show("Voertuig is toegevoegd!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelecteerBrandstof_Click(object sender, RoutedEventArgs e)
        {
            new BenzineSelecteren().ShowDialog();
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

        private void lstBrandtof_Loaded(object sender, RoutedEventArgs e)
        {
            if (_brandstoffen == null && _brandstoffen.Count == 0) { lstBrandtof.ItemsSource = null; return; }
            lstBrandtof.ItemsSource = _brandstoffen;
        }

        private void miVerwijder_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                IReadOnlyList<VoertuigType> types = _voertuigTypeManager.GeefAlleVoertuigen();
                ObservableCollection<VoertuigType> ts = new();
                foreach (var type in types)
                {
                    ts.Add(type);
                }
                cmbType.ItemsSource = ts;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }
    }
}
