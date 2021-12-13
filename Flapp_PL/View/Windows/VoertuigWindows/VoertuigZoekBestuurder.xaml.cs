using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.VoertuigWindow;
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
    /// Interaction logic for VoertuigZoekBestuurder.xaml
    /// </summary>
    public partial class VoertuigZoekBestuurder : Window
    {
        ObservableCollection<Bestuurder> _bestuurders = new();
        private BestuurderManager _bestuurderManager;
        private VoertuigToevoegen _ttw;
        private Bestuurder _bestuurder;

        public VoertuigZoekBestuurder()
        {
            InitializeComponent();
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadWaarden();
           // _ttw = ttw;
        }

        private void laadWaarden()
        {
            //lstBestuurders.ItemsSource = (List<Bestuurder>)_bestuurderManager.GeefBestuurdersZonderVoertuig();
        }

        private void VoegBestuurderToe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Bestuurder> bestuurders = new List<Bestuurder>();
                bestuurders.Add((Bestuurder)lstBestuurders.SelectedItem);

                _ttw.lstBestuurder.ItemsSource = bestuurders;
                Close();
            }
            catch (Exception) { throw; }
        }

        private void btnZoekBestuurder_Click(object sender, RoutedEventArgs e)
        {
            if (lstBestuurders.SelectedItem == null) { MessageBox.Show("Er is geen Brandstof geselecteerd", Title, MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            try
            {
                _bestuurders.Add((Bestuurder)lstBestuurders.SelectedItem);
                Application.Current.Properties["Bestuurder"] = _bestuurders;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error); }
            Close();
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
