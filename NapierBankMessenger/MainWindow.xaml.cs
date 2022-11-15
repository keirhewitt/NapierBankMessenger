using NapierBankMessenger.MVVM.FileIO;
using NapierBankMessenger.MVVM.ViewModel;
using System.Windows;


namespace NapierBankMessenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModelController VMC;

        public MainWindow()
        {
            InitializeComponent();
            Textspeak.IO(); // Initialise Textspeak method to open CSV file
            VMC = new ViewModelController();
            this.DataContext = VMC;
        }

        // Open window that shows end of session details
        private void ButtonExit(object sender, RoutedEventArgs e)
        {
            VMC.OnAppExit = true;  
        }

        // Maximimse the screen, if maximised, return to normal
        private void ButtonMaximise(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else { Application.Current.MainWindow.WindowState = WindowState.Maximized; }
        }

        // Minimise program to taskbar
        private void ButtonMinimise(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
