using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CoronaWatchUI.Domain;
using MaterialDesignThemes.Wpf;

namespace CoronaWatchUI
{
    /// <summary>
    /// Interaction logic for TestingWindows.xaml
    /// </summary>
    public partial class TestingWindows : Window
    {
        public static Snackbar Snackbar;
        public TestingWindows()
        {
            /*
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2500);
            }).ContinueWith(t =>
            {
                MainSnackbar.MessageQueue.Enqueue("Welcome to CoronaWatch");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue);

            Snackbar = this.MainSnackbar;
            */
        }
    }
}
