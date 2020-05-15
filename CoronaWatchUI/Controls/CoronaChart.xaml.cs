using CoronaWatchUI.Domain;
using System.Windows;
using System.Windows.Controls;

namespace CoronaWatchUI.Controls
{
    /// <summary>
    /// Interaction logic for CoronaChart.xaml
    /// </summary>
    public partial class CoronaChart : UserControl
    {
        public CoronaChart()
        {
            InitializeComponent();
        }

        private void btn_viewChart_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CoronaChartViewModel;
            var series = viewModel.UpdateSeries();
            cht_countryChart.Series = series;
        }
    }
}
