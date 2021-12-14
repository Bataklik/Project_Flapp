using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Flapp_PL.View.Windows.BestuurderWindows.BeheerWindows
{
    public partial class Adresbeheer : Window
    {
        private AdresManager _adresManager;

        public Adresbeheer(VoegBestuurderToe parentWindow, Adres _adres)
        {
            InitializeComponent();
            _adresManager = new AdresManager(new AdresRepo(Application.Current.Properties["User"].ToString()));

        }

        private void txtPostcode_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelecteer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstAdressen_Loaded(object sender, RoutedEventArgs e)
        {
            lstAdressen.Items.Clear();
            try
            { lstAdressen.ItemsSource = _adresManager.GeefAdressen(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
