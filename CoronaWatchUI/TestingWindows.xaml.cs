using CoronaWatchLibrary.ViewModels;
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
using System.Windows.Shapes;

namespace CoronaWatchUI
{
    /// <summary>
    /// Interaction logic for TestingWindows.xaml
    /// </summary>
    public partial class TestingWindows : Window
    {
        public TestingWindows()
        {
            CoronaWatchViewModel region = new CoronaWatchViewModel();
            InitializeComponent();
            DataContext = region;
        }
    }
}
