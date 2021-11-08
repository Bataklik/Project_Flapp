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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flapp_PL.View.UserControls
{
    /// <summary>
    /// Interaction logic for VoertuigUC.xaml
    /// </summary>
    public partial class VoertuigUC : UserControl
    {
        private VoertuigManager _voertuigManager;
        private string _connStringRaf = @"Data Source=LAPTOP-4QVTNHR0\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";

        public VoertuigUC()
        {
            InitializeComponent();
            _voertuigManager = new VoertuigManager(new VoertuigRepo(_connStringRaf));
            laadVoertuigen();
        }

        private void laadVoertuigen()
        {
            IReadOnlyList<Voertuig> voertuigen;
            try
            {
                voertuigen = _voertuigManager.GeefAlleVoertuigen();
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            lstVoertuigen.ItemsSource = voertuigen;
        }

        private void btnVoegVoertuigToe_Click(object sender, RoutedEventArgs e)
        {
            Voertuig v = new Voertuig(txtMerk.Text, txtModel.Text, txtChassis.Text, txtNummerplaat.Text, txtType.Text, txtKleur.Text, Convert.ToInt32(txtDeuren.Text));
            try
            {
                _voertuigManager.VoegVoertuigToe(v);
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }
    }
}
