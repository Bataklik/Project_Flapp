using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Collections.ObjectModel;
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
            if (string.IsNullOrWhiteSpace(txtNaam.Text)) { MessageBox.Show("Velden zijn leeg!"); return; }
            try { _brandstofManager.VoegBrandstofToe(new Brandstof(txtNaam.Text)); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
