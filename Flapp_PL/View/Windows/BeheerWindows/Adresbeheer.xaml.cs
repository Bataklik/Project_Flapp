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
        private VoegBestuurderToe _parentWindow;
        private AdresManager _adresManager;

        public Adresbeheer(VoegBestuurderToe parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            _adresManager = new AdresManager(new AdresRepo(Application.Current.Properties["User"].ToString()));
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelecteer_Click(object sender, RoutedEventArgs e)
        {
            if (lstAdressen.SelectedItems == null) { MessageBox.Show("U heeft geen adres geselecteerd!"); return; }
            _parentWindow.lstAdres.Items.Clear();
            _parentWindow.lstAdres.Items.Add((Adres)lstAdressen.SelectedItem);
            Close();
        }

        private void lstAdressen_Loaded(object sender, RoutedEventArgs e)
        {
            lstAdressen.Items.Clear();
            try
            { lstAdressen.ItemsSource = _adresManager.GeefAdressen(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void VerwijderAdres_Click(object sender, RoutedEventArgs e)
        {
            if (lstAdressen.SelectedItem == null) { MessageBox.Show("U heeft geen adres geselecteerd"); }
        }

        private void VoegAdresToe_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
