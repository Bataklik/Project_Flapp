using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using Flapp_PL.View.Windows.BestuurderWindows;
using Flapp_PL.View.Windows.VoertuigWindow;
using Flapp_PL.View.Windows.VoertuigWindows;
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
    /// <summary>
    /// Interaction logic for Bestuurderbeheer.xaml
    /// </summary>
    
    public partial class Bestuurderbeheer : Window
    {
        private VoertuigToevoegen _parentWindow;

        private VoertuigUpdaten _parentUpdateWindow;

        private BestuurderManager _bestuurderManager;
        public Bestuurderbeheer(VoertuigToevoegen parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadBestuurders();
        }
        public Bestuurderbeheer(VoertuigUpdaten parentWindow)
        {
            InitializeComponent();
            _parentUpdateWindow = parentWindow;
            _bestuurderManager = new BestuurderManager(new BestuurderRepo(Application.Current.Properties["User"].ToString()));
            laadBestuurders();
        }

        public void laadBestuurders()
        {
            try
            {
                lstBestuurder.ItemsSource = _bestuurderManager.GeefAlleBestuurdersZonderVoertuig().Values;
            }
            catch (Exception ex) {  MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        private void miVoegToe_Click(object sender, RoutedEventArgs e)
        {
           // new VoegBestuurderToe().ShowDialog();
        }
        private void miVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lstBestuurder.SelectedItem == null) { MessageBox.Show("U heeft geen tankkaart geselecteerd"); }
            try
            {
                _bestuurderManager.VerwijderBestuurder((Bestuurder)lstBestuurder.SelectedItem);
                laadBestuurders();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void miSelecteer_Click(object sender, RoutedEventArgs e)
        {
            if (lstBestuurder.SelectedItems == null) { MessageBox.Show("U heeft geen bestuurder geselecteerd!"); return; }
            if (_parentWindow == null)
            {
                _parentUpdateWindow.lstBestuurder.Items.Clear();
                _parentUpdateWindow.lstBestuurder.Items.Add((Bestuurder)lstBestuurder.SelectedItem);
            }
            else
            {
                _parentWindow.lstBestuurder.Items.Clear();
                _parentWindow.lstBestuurder.Items.Add((Bestuurder)lstBestuurder.SelectedItem);
            }
            Close();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
