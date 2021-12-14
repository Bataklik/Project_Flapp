using Flapp_BLL.Models;
using System.Text.RegularExpressions;
using System.Windows;

namespace Flapp_PL.View.Windows.BestuurderWindows.BeheerWindows
{
    public partial class Adresbeheer : Window
    {
        public Adresbeheer(Adres _adres)
        {
            InitializeComponent();
        }

        private void txtPostcode_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
