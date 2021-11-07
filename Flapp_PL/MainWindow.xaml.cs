using Flapp_PL.View.UserControls;
using System.Windows;
namespace Flapp_PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBestuurder_Click(object sender, RoutedEventArgs e)
        {
            BestuurderUC bestuurderUc = new BestuurderUC();
            wpUserControl.Children.Clear();
            wpUserControl.Children.Add(bestuurderUc);
        }
        private void btnVoertuig_Click(object sender, RoutedEventArgs e)
        {
            VoertuigUC voertuigUc = new VoertuigUC();
            wpUserControl.Children.Clear();
            wpUserControl.Children.Add(voertuigUc);
        }
        private void btnTankkaart_Click(object sender, RoutedEventArgs e)
        {
            TankkaartUC tankkaartUc = new TankkaartUC();
            wpUserControl.Children.Clear();
            wpUserControl.Children.Add(tankkaartUc);
        }
    }
}
