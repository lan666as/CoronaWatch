using CoronaWatchUI.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CoronaWatchUI.Domain
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            _allItems = GenerateMenuItems(snackbarMessageQueue);
            FilterItems(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _searchKeyword;
        private ObservableCollection<MenuItem> _allItems;
        private ObservableCollection<MenuItem> _menuItems;
        private MenuItem _selectedItem;


        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MenuItems)));
                FilterItems(_searchKeyword);
            }
        }

        public ObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MenuItems)));
            }
        }

        public MenuItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value == null || value.Equals(_selectedItem)) return;

                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }


        private ObservableCollection<MenuItem> GenerateMenuItems(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null)
                throw new ArgumentNullException(nameof(snackbarMessageQueue));

            return new ObservableCollection<MenuItem>
            {
                new MenuItem("Home", "Home", new Home()),
                new MenuItem("World", "Earth", new World{ DataContext = new WorldGridsViewModel()}),
                new MenuItem("Chart", "ChartTimelineVariant", new CoronaChart{ DataContext = new CoronaChartViewModel()}),
                new MenuItem("Map", "Map", new CoronaMap() { DataContext = new CoronaMapViewModel() } ),
                new MenuItem("Feed", "Rss", new Feed() { DataContext = new FeedViewModel()}),
                new MenuItem("About", "Information", new About())
            };
        }

        private void FilterItems(string keyword)
        {
            var filteredItems =
                string.IsNullOrWhiteSpace(keyword) ?
                _allItems :
                _allItems.Where(i => i.Name.ToLower().Contains(keyword.ToLower()));

            MenuItems = new ObservableCollection<MenuItem>(filteredItems);
        }
    }
}
