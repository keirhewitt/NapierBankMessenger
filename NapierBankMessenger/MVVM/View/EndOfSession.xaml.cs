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
    /// Handles the end of program logic.
    /// Will display the trending data at the end.
    /// Click the button at the bottom of View to exit program.
    /// </summary>
    public partial class EndOfSession : UserControl
    {
        Controller Ctrl;
        EndOfSessionViewModel EOS;

        public EndOfSession(Controller ctrl)
        {
            InitializeComponent();
            EOS = new EndOfSessionViewModel(ctrl);
            Ctrl = ctrl;
            DataContext = Ctrl;
        }

        // When program finishes, send data to json and then shutdown application
        private void OnFinish(object sender, RoutedEventArgs e)
        {
            Ctrl.SendToJSON();
            Application.Current.Shutdown();
        }
    }
}
