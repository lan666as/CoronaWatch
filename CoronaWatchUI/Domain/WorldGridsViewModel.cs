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

namespace CoronaWatchUI.Domain
{
    public class WorldGridsViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<RegionGridsViewModel> _items;

        public WorldGridsViewModel()
        {
            _items = GenerateData();

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

        private static ObservableCollection<RegionGridsViewModel> GenerateData()
        {
            CoronaWatchContext context = new CoronaWatchContext();
            
            if(context.ReportDBs.Count() == 0)
            {
                JHUDataService.UpdateDatabase();
            }

            List<Region> regions = JHUDataService.FetchDatabase();
            ObservableCollection<RegionGridsViewModel> regionGrids = new ObservableCollection<RegionGridsViewModel>();

            foreach(Region reg in regions)
            {
                RegionGridsViewModel regGrid = new RegionGridsViewModel(reg);
                regionGrids.Add(regGrid);
            }
            return regionGrids;
        }

        public ObservableCollection<RegionGridsViewModel> Items => _items;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
