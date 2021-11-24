using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Windows;

namespace Flapp_PL.View.Windows.VoertuigWindow
{
    public partial class BenzineToevoegen : Window
    {
        private BrandstofManager _brandstofManager;

        public Brandstof br;
        public BenzineToevoegen()
        {
            InitializeComponent();
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));

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
