using MaterialDesignThemes.Wpf;
using System.Windows;

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
