using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Flapp_PL.View.Windows.VoertuigWindow
{
    /// <summary>
    /// Interaction logic for BenzineToevoegen.xaml
    /// </summary>
    public partial class BenzineToevoegen : Window
    {
        private BrandstofManager _brandstofManager;

        public Brandstof br;
        public BenzineToevoegen()
        {
            InitializeComponent();
            _brandstofManager = new BrandstofManager(new BrandstofRepo(ConfigurationManager.ConnectionStrings["connString"].ConnectionString));

        }
        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            if (!string.IsNullOrWhiteSpace(txtNaam.Text))
            {
                main.Show();
                Brandstof b = new Brandstof(txtNaam.Text);
                try
                {
                    _brandstofManager.VoegBrandstofToe(b);
                }
                catch (Exception ex) { throw new Exception(ex.Message, ex); }
                Close();
                return;
            }
            MessageBox.Show("Velden zijn leeg!");
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
