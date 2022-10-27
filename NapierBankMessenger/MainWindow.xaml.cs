using NapierBankMessenger.MVVM.ViewModel;
using System.Windows;


namespace NapierBankMessenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModelController();

        }
    }
}
