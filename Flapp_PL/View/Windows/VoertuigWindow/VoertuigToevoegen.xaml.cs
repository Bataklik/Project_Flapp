using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Windows;
using System.Configuration;
using System.Collections.ObjectModel;

namespace Flapp_PL.View.Windows.VoertuigWindow
{
    public partial class VoertuigToevoegen : Window
    {
        private VoertuigManager _voertuigManager;
        private ObservableCollection<Brandstof> _brandstoffen;
        public VoertuigToevoegen()
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(ConfigurationManager.ConnectionStrings["connStringR"].ConnectionString));
            if (Application.Current.Properties["Brandstof"] == null)
            {
                Application.Current.Properties["Brandstof"] = new ObservableCollection<Brandstof>();
                _brandstoffen = (ObservableCollection<Brandstof>)Application.Current.Properties["Brandstof"]; ;
            }
            else { _brandstoffen = (ObservableCollection<Brandstof>)Application.Current.Properties["Brandstof"]; ; }
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            Voertuig v = new Voertuig(txtMerk.Text, txtModel.Text, txtChassis.Text, txtNummerplaat.Text, txtType.Text, txtKleur.Text, Convert.ToInt32(txtDeuren.Text));
            try
            {
                _voertuigManager.VoegVoertuigToe(v);
                MessageBox.Show("Voertuig is toegevoegd!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            new MainWindow().Show();
            Close();
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void SelecteerBrandstof_Click(object sender, RoutedEventArgs e)
        {
            new BenzineSelecteren().Show();
            Close();
        }

        private void VerwijderBrandstof_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Brandstof x = (Brandstof)lstBrandtof.SelectedItem;
                int brandstof = x.Id;
                _brandstoffen.Remove(x);
                Application.Current.Properties["Brandstoff"] = _brandstoffen;
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
            if (_brandstoffen != null && _brandstoffen.Count != 0)
            {
                //ObservableCollection<Brandstof> oc = new();
                //foreach (var b in _Brandstoffen)
                //{
                //    oc.Add(b);
                //}
                lstBrandtof.ItemsSource = _brandstoffen;
            }
        }

        private void miVerwijder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
