using NapierBankMessenger.MVVM.Model;
using NapierBankMessenger.MVVM.ViewModel;
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

namespace NapierBankMessenger.MVVM.View
{
    /// <summary>
    /// Interaction logic for EndOfSession.xaml
    /// </summary>
    public partial class EndOfSession : UserControl
    {
        public EndOfSession(Controller ctrl)
        {
            InitializeComponent();
            DataContext = new EndOfSessionViewModel(ctrl);
        }

        private void OnFinish(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
