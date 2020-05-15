using Esri.ArcGISRuntime.Mapping;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoronaWatchUI.Domain
{
    class CoronaMapViewModel : INotifyPropertyChanged
    {
        public CoronaMapViewModel()
        {
            LoadWebMap();
        }

        /// <summary>
        /// Opens a web map stored in ArcGIS Online and uses it to set the MapViewModel.Map property
        /// </summary>
        private void LoadWebMap()
        {
            var itemId = "c1a139e47e5640ca9ab4b3acc58077ef";
            var webMapUrl = string.Format("http://www.arcgis.com/sharing/rest/content/items/{0}/data", itemId);
            Esri.ArcGISRuntime.Mapping.Map webMap = new Map(new System.Uri(webMapUrl));
            Map = webMap;
        }

        private Map _map;

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get { return _map; }
            set { _map = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
