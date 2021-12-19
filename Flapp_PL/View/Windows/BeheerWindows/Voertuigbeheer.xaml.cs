using Flapp_BLL.Managers;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.BestuurderWindows;
using System;
using System.Collections.Generic;
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

namespace Flapp_PL.View.Windows.BeheerWindows
{
    public partial class Voertuigbeheer : Window
    {
        private VoegBestuurderToe _parentWindow;
        private VoertuigManager _voertuigManager;

        public Voertuigbeheer(VoegBestuurderToe parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            _voertuigManager = new VoertuigManager(new VoertuigRepo(Application.Current.Properties["User"].ToString()));
            LaadVoertuigen();
        }

        private void LaadVoertuigen()
        {
            try
            {
                lstVoertuigen.ItemsSource = _voertuigManager.GeefVoertuigen().Values;
                cbMerk.ItemsSource = _voertuigManager.GeefMerken();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void miVoegToe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miVerwijderen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miSelecteer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbMerk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMerk.SelectedItem == null) { MessageBox.Show("Geen Juiste merk aangeduid!"); return; }
            try { cbModel.ItemsSource = _voertuigManager.GeefModellenMerk((string)cbMerk.SelectedItem); }
            catch (Exception) { throw; }
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            if (cbMerk.SelectedItem == null || cbModel.SelectedItem == null) { MessageBox.Show("Geen Juiste merk of model aangeduid!"); return; }
            var merkModel = new { Merk = (string)cbMerk.SelectedItem, Model = (string)cbModel.SelectedItem };
            lstVoertuigen.ItemsSource = _voertuigManager.ZoekVoertuigen(merkModel.Merk, merkModel.Model);
        }
    }
}
