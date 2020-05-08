using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoronaWatchLibrary;
using CoronaWatchLibrary.ViewModels;

namespace CoronaWatchUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            CoronaWatchViewModel region = new CoronaWatchViewModel();
            region.Regions.Sort((x, y) => x.Name.CompareTo(y.Name));
            DataContext = region;
            InitializeComponent();
            
        }
    }
}
