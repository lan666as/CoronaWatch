using CoronaWatchLibrary;
using CoronaWatchUI.Domain;
using System.Windows;
using System.Windows.Controls;

namespace CoronaWatchUI.Controls
{
    /// <summary>
    /// Interaction logic for World.xaml
    /// </summary>
    public partial class World : UserControl
    {
        public World()
        {
            InitializeComponent();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if (tbk_LastUpdate.Text != System.DateTime.Today.ToString("M/dd/yyyy"))
            {
                try
                {
                    JHUDataService.UpdateDatabase();
                }
                finally
                {
                    dtg_World.ItemsSource = WorldGridsViewModel.GenerateData();
                    tbk_LastUpdate.Text = System.DateTime.Today.ToString("M/dd/yyyy");
                }
            }
            else
            {
                MessageBox.Show("Already Up to date");
            }
        }
    }
}
