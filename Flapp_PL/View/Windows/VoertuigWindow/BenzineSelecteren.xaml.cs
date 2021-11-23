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
            _brandstofManager = new BrandstofManager(new BrandstofRepo(ConfigurationManager.ConnectionStrings["connStringB"].ConnectionString));
            laadBrandstoffen();
        }
        private void laadBrandstoffen()
        {
            //IReadOnlyList<Brandstof> brandstoffen;
            try
            {
                IReadOnlyList<Brandstof> brandstoffen = _brandstofManager.GeefAlleBrandstoffen();
                ObservableCollection<Brandstof> ts = new();
                foreach (var brandstof in brandstoffen)
                {
                    ts.Add(brandstof);
                }
                lstBrandstof.ItemsSource = ts;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }

        }
        private void btnSelecteren_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstBrandstof.SelectedItem != null)
                {
                    Brandstof b = (Brandstof)lstBrandstof.SelectedItem;
                    _brandstoffen.Add(b);
                    Application.Current.Properties["Brandstof"] = _brandstoffen;
                    //Window myWindow = GetWindow(this);
                    new VoertuigToevoegen().Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Er is geen Brandstof geselecteerd", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            new VoertuigToevoegen().Show();
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["Brandstof"] != null)
            {
                _brandstoffen = (ObservableCollection<Brandstof>)Application.Current.Properties["Brandstof"]; ;
            }
        }
    }
}
