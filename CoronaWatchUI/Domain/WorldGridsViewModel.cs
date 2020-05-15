using CoronaWatchDB;
using CoronaWatchLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoronaWatchUI.Domain
{
    public class WorldGridsViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<RegionGridsViewModel> _items;
        static private DateTime _lastUpdate;

        public WorldGridsViewModel()
        {
            _items = GenerateData();

            ItemsView.Filter = new Predicate<object>(o => Filter(o as RegionGridsViewModel));
            ItemsView.Refresh();

            /*
            foreach (var model in _items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(RegionGridsViewModel.IsSelected))
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                };
            }
            */
        }

        /*
        public bool? IsAllItemsSelected
        {
            get
            {
                var selected = _items.Select(item => item.IsSelected).Distinct().ToList();
                return selected.Count == 1 ? selected.Single() : (bool?)null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                }
            }
        }

        private static void SelectAll(bool select, IEnumerable<RegionGridsViewModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }
        */

        


        public static ObservableCollection<RegionGridsViewModel> GenerateData()
        {
            CoronaWatchContext context = new CoronaWatchContext();

            context.Database.EnsureCreated();
            if (!context.ReportDBs.Any())
            {
                JHUDataService.UpdateDatabase();
            }

            List<Region> regions = JHUDataService.FetchDatabase();
            ObservableCollection<RegionGridsViewModel> regionGrids = new ObservableCollection<RegionGridsViewModel>();

            foreach (Region reg in regions)
            {
                RegionGridsViewModel regGrid = new RegionGridsViewModel(reg);
                regionGrids.Add(regGrid);
            }

            _lastUpdate = Convert.ToDateTime(context.ReportDBs.OrderByDescending(r => r.Date).FirstOrDefault().Date);
            return regionGrids;
        }

        public ObservableCollection<RegionGridsViewModel> Items => _items;
        public DateTime LastUpdate
        {
            get { return _lastUpdate; }
            set
            {
                _lastUpdate = value;
                OnPropertyChanged("");
            }
        }


        public ICollectionView ItemsView
        {
            get { return CollectionViewSource.GetDefaultView(Items); }
        }

        private bool Filter(RegionGridsViewModel region)
        {
            return Search == null
                || region.Name.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1;
        }

        private string _search;

        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged();
                ItemsView.Refresh();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
