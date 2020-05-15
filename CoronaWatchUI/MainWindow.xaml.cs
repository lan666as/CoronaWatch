using CoronaWatchUI.Domain;
using MaterialDesignExtensions.Controls;
using MaterialDesignThemes.Wpf;
using System.Threading;
using System.Threading.Tasks;

namespace CoronaWatchUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MaterialWindow
    {
        public static Snackbar Snackbar;
        public MainWindow()
        {
            InitializeComponent();

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2500);
            }).ContinueWith(t =>
            {
                MainSnackbar.MessageQueue.Enqueue("Welcome to CoronaWatch");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue);

            Snackbar = this.MainSnackbar;

        }
    }
}
