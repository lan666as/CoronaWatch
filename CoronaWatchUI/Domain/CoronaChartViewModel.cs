using CoronaWatchDB;
using CoronaWatchLibrary;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchUI.Domain
{
    class CoronaChartViewModel : INotifyPropertyChanged
    {
        private SeriesCollection _series;
        private List<Region> _regions;
        private Region _selectedRegion;


        public CoronaChartViewModel()
        {
            _selectedRegion = new Region{ Name="Indonesia", Slug = "indonesia", ISOCode="ID"}; // Default value for chart
            _series = GenerateData(_selectedRegion);
            _regions = GetRegion();
            YFormatter = value => value.ToString();
        }

        private SeriesCollection GenerateData(Region region)
        {
            
            TimeSeries timeSeries = JHUDataService.FetchTimeSeriesByRegion(region);

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
        private List<Region> GetRegion()
        {
            using(CoronaWatchContext context = new CoronaWatchContext())
            {
                List<Region> regions = new List<Region>();
                List<RegionDB> regionDBs = context.RegionDBs.ToList();
                foreach(RegionDB regionDB in regionDBs)
                {
                    Region region = new Region()
                    {
                        Name = regionDB.Name,
                        Slug = regionDB.Slug,
                        ISOCode = regionDB.ISOCode,
                    };
                    regions.Add(region);
                }
                return regions;
            }
        }

        public SeriesCollection UpdateSeries()
        {
            _series = GenerateData(SelectedRegion);
            return _series;
        }

        public SeriesCollection Series
        {
            get { return _series; }
            set
            {
                _series = value;
                OnPropertyChanged("Series");
            }
        }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public List<Region> Regions => _regions;
        public Region SelectedRegion
        {
            get
            {
                return _selectedRegion;
            }
            set
            {
                _selectedRegion = value;
                OnPropertyChanged("SelectedRegion");
            }
        }
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
