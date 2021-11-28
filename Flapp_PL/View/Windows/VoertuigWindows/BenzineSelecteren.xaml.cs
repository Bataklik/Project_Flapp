using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;

namespace Flapp_PL.View.Windows.VoertuigWindow
{
    public partial class BenzineSelecteren : Window
    {
        ObservableCollection<Brandstof> _brandstoffen = new();
        private BrandstofManager _brandstofManager;

        public BenzineSelecteren()
        {
            InitializeComponent();
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
            laadBrandstoffen();
        }
        private void laadBrandstoffen()
        {
            try { lstBrandstof.ItemsSource = (List<Brandstof>)_brandstofManager.GeefAlleBrandstoffen(); }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }
        private void btnSelecteren_Click(object sender, RoutedEventArgs e)
        {
            if (lstBrandstof.SelectedItem == null) { MessageBox.Show("Er is geen Brandstof geselecteerd", Title, MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            try
            {
                _brandstoffen.Add((Brandstof)lstBrandstof.SelectedItem);
                Application.Current.Properties["Brandstof"] = _brandstoffen;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error); }
            Close();
        }
        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["Brandstof"] != null) { _brandstoffen = (ObservableCollection<Brandstof>)Application.Current.Properties["Brandstof"]; ; }
        }
    }
}
