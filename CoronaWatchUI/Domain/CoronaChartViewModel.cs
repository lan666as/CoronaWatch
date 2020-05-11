using CoronaWatchLibrary;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchUI.Domain
{
    class CoronaChartViewModel
    {
        SeriesCollection _series;

        public CoronaChartViewModel()
        {
            _series = GenerateData();
            YFormatter = value => value.ToString();
        }

        private SeriesCollection GenerateData()
        {
            TimeSeries timeSeries = JHUDataService.FetchTimeSeriesByRegion(new Region(slug: "indonesia"));

            Labels = timeSeries.DateSeries.Select(i => i.ToString()).ToArray();

            return new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Active Cases",
                    Values = timeSeries.StatisticSeries.Select(i => i.ActiveCases).ToArray().AsChartValues(),
                },
                new LineSeries
                {
                    Title = "Confirmed Cases",
                    Values = timeSeries.StatisticSeries.Select(i => i.ConfirmedCases).ToArray().AsChartValues(),
                },
                new LineSeries
                {
                    Title = "Death Cases",
                    Values = timeSeries.StatisticSeries.Select(i => i.DeathCases).ToArray().AsChartValues(),
                },
                new LineSeries
                {
                    Title = "Recovered Cases",
                    Values = timeSeries.StatisticSeries.Select(i => i.RecoveredCases).ToArray().AsChartValues(),
                },
            };
        }

        public SeriesCollection Series => _series;
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
