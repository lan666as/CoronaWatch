using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary.ViewModels
{
    public class CoronaWatchViewModel : INotifyPropertyChanged
    {
        public List<Region> Regions { get; } = JHUDataService.FetchAllRegion();
        public List<Report> Reports { get; } = JHUDataService.FetchRegionSummary();

        private int selectedRegionIndex;
        private Region selectedRegion;
        private Report selectedRegionReport;

        public int SelectedRegionIndex
        {
            get { return selectedRegionIndex; }
            set
            {
                if (0 <= value && value < Regions.Count())
                {
                    selectedRegionIndex = Convert.ToInt32(value);
                    OnPropertyChanged("SelectedRegionIndex");
                    OnPropertyChanged("SelectedRegion");
                    OnPropertyChanged("SelectedRegionReport");
                }
            }
        }

        public Region SelectedRegion
        {
            get { return selectedRegion = Regions[selectedRegionIndex]; }
            set
            {
                selectedRegion = value;
                OnPropertyChanged("SelectedRegion");
            }
        }

        public Report SelectedRegionReport
        {
            get { return selectedRegionReport = Reports.Where(r => r.Statistic.StatisticID == SelectedRegion.ISOCode).FirstOrDefault(); }
            set
            {
                selectedRegionReport = value;
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
