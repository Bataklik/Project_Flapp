using System.Text.RegularExpressions;
using System.Windows;

namespace Flapp_PL.View.Windows.BestuurderWindows.BeheerWindows
{
    /// <summary>
    /// Interaction logic for Adresbeheer.xaml
    /// </summary>
    public partial class Adresbeheer : Window
    {
        public Adresbeheer()
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
