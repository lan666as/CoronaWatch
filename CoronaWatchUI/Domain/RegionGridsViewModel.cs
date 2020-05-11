using CoronaWatchLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchUI.Domain
{
    public class RegionGridsViewModel : INotifyPropertyChanged
    {
        //private bool _isSelected;
        private string _name;
        private int _confirmedCases;
        private int _recoveredCases;
        private int _deathCases;
        private int _activeCases;

        public RegionGridsViewModel(Region region)
        {
            this.Name = region.Name;
            this.ConfirmedCases = region.Report.Statistic.ConfirmedCases;
            this.RecoveredCases = region.Report.Statistic.RecoveredCases;
            this.DeathCases = region.Report.Statistic.DeathCases;
            this.ActiveCases = region.Report.Statistic.ActiveCases;
        }

        /*
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }
        */
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }
        public int ConfirmedCases
        {
            get { return _confirmedCases; }
            set
            {
                if (_confirmedCases == value) return;
                _confirmedCases = value;
                OnPropertyChanged();
            }
        }
        public int RecoveredCases
        {
            get { return _recoveredCases; }
            set
            {
                if (_recoveredCases == value) return;
                _recoveredCases = value;
                OnPropertyChanged();
            }
        }
        public int DeathCases
        {
            get { return _deathCases; }
            set
            {
                if (_deathCases == value) return;
                _deathCases = value;
                OnPropertyChanged();
            }
        }
        public int ActiveCases
        {
            get { return _activeCases; }
            set
            {
                if (_activeCases == value) return;
                _activeCases = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
