using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
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
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Flapp_PL.View.Windows.VoertuigWindow
{
    /// <summary>
    /// Interaction logic for VoertuigToevoegen.xaml
    /// </summary>
    public partial class VoertuigToevoegen : Window
    {
        private VoertuigManager _voertuigManager;
        private BrandstofManager _brandstofManager;
        private List<Brandstof> _Brandstoffen = (List<Brandstof>)Application.Current.Properties["Brandstof"];
        public VoertuigToevoegen()
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(ConfigurationManager.ConnectionStrings["connStringB"].ConnectionString));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(ConfigurationManager.ConnectionStrings["connStringB"].ConnectionString));
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Voertuig v = new Voertuig(txtMerk.Text, txtModel.Text, txtChassis.Text, txtNummerplaat.Text, txtType.Text, txtKleur.Text, Convert.ToInt32(txtDeuren.Text));
            try
            {
                _voertuigManager.VoegVoertuigToe(v);
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            MessageBox.Show("Voertuig is toegevoegd!");
            main.Show();
            Close();
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void SelecteerBrandstof_Click(object sender, RoutedEventArgs e)
        {
            BenzineSelecteren main = new BenzineSelecteren();
            main.Show();
            Close();
        }

        private void DgvBrandstofTypes_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Brandstoffen != null && _Brandstoffen.Count != 0)
            {
                BenzineSelecteren bs = new BenzineSelecteren();
                ObservableCollection<Brandstof> oc = new();
                foreach (var truitje in _Brandstoffen)
                {
                    oc.Add(truitje);
                }
                lstBrandtof.ItemsSource = oc;
                //Price_Loaded(sender, e);
            }
            else
            {
                lstBrandtof.ItemsSource = null;
            }
        }

        private void VerwijderBrandstof_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Brandstof x = (Brandstof)lstBrandtof.SelectedItem;
                int brandstof = x.Id;
                _Brandstoffen.Remove(x);
                Application.Current.Properties["Brandstoff"] = _Brandstoffen;
                MessageBox.Show("Brandstof is verwijderd van de wagen", Title, MessageBoxButton.OK, MessageBoxImage.Information);
                DgvBrandstofTypes_Loaded(sender, e);
                //Price_Loaded(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
