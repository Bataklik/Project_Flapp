using Flapp_BLL.Managers;
using Flapp_BLL.Models;
using Flapp_DAL.Repository;
using System;
using System.Configuration;
using System.Windows;

namespace Flapp_PL.View.Windows.TankkaartWindows
{

    public partial class TankkaartToevoegenWindow : Window
    {
        private TankkaartManager _tankkaartManager;
        private BrandstofManager _brandstofManager;

        public TankkaartToevoegenWindow()
        {
            InitializeComponent();
            _tankkaartManager = new TankkaartManager(new TankkaartRepo(Application.Current.Properties["User"].ToString()));
            _brandstofManager = new BrandstofManager(new BrandstofRepo(Application.Current.Properties["User"].ToString()));
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpGeldigheidsdatum.SelectedDate != null && !string.IsNullOrWhiteSpace(txtPincode.Text))
                {
                    Tankkaart t = new Tankkaart(txtPincode.Text, Convert.ToDateTime(dpGeldigheidsdatum.SelectedDate));
                    //_tankkaartManager.VoegTankkaartToe(t);
                    MessageBox.Show("Tankkaart toegevoegd!");
                }
                else { MessageBox.Show("Velden zijn leeg!"); }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnBestuurder_Click(object sender, RoutedEventArgs e)
        {
            TankkaartToevoegenWindow ttw = this;
            
            new TankkaartZoekBestuurderWindow(ttw).ShowDialog();
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnVoegBrandstofToe_Click(object sender, RoutedEventArgs e) {
            if ((Brandstof)cbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (lbBrandstof.Items.Contains((Brandstof)cbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
            lbBrandstof.Items.Add((Brandstof)cbBrandstoffen.SelectedItem);
        }

        private void btnVerwijderBrandstof_Click(object sender, RoutedEventArgs e) {
            if ((Brandstof)cbBrandstoffen.SelectedItem == null) { MessageBox.Show("U heeft geen brandstof aangeduid!"); return; }
            if (lbBrandstof.Items.Contains((Brandstof)cbBrandstoffen.SelectedItem)) { MessageBox.Show("Brandstof staat al op de lijst!"); return; }
            lbBrandstof.Items.Remove((Brandstof)cbBrandstoffen.SelectedItem);
        }

        private void cbBrandstoffen_Loaded(object sender, RoutedEventArgs e) {
            try {
                cbBrandstoffen.ItemsSource = _brandstofManager.GeefAlleBrandstoffen();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
