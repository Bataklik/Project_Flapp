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
            MainWindow main = this;
            wpUserControl.Children.Clear();
            BestuurderUC bestuurderUc = new BestuurderUC(main);
            wpUserControl.Children.Add(bestuurderUc);
        }
        private void btnVoertuig_Click(object sender, RoutedEventArgs e)
        {
            wpUserControl.Children.Clear();
            VoertuigUC voertuigUc = new VoertuigUC();
            wpUserControl.Children.Add(voertuigUc);
        }
        private void btnTankkaart_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = this;
            wpUserControl.Children.Clear();
            TankkaartUC tankkaartUc = new TankkaartUC(main);
            wpUserControl.Children.Add(tankkaartUc);
        }
    }
}
