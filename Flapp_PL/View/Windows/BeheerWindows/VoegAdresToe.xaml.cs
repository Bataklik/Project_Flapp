using Flapp_BLL.Managers;
using Flapp_PL.View.Windows.BestuurderWindows.BeheerWindows;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Flapp_PL.View.Windows.BeheerWindows
{
    public partial class VoegAdresToe : Window
    {
        private Adresbeheer _ab;
        private AdresManager _am;

        public VoegAdresToe(Adresbeheer adresbeheer, AdresManager adresManager)
        {
            InitializeComponent();
            _ab = adresbeheer;
            _am = adresManager;
        }
        private void txtPostcode_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStraat.Text) || string.IsNullOrWhiteSpace(txtHuisnummer.Text) || string.IsNullOrWhiteSpace(txtStad.Text) || string.IsNullOrWhiteSpace(txtPostcode.Text)) { MessageBox.Show("Niet alle velden zijn ingegeven!", "Lege velden", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            try
            {
                _am.VoegAdresToe(new(txtStraat.Text, txtHuisnummer.Text, txtStad.Text, int.Parse(txtPostcode.Text)));
                _ab.LaadAdressen();
                Close();
                MessageBox.Show("Adres toegevoegd!", "Toevoegen gelukt!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error); ; }
        }
    }
}
