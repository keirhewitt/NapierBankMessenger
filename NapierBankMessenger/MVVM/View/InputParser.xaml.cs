using NapierBankMessenger.MVVM.ViewModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace NapierBankMessenger.MVVM.View
{
    /// <summary>
    /// Interaction logic for InputParser.xaml
    /// </summary>
    public partial class InputParser : UserControl
    {
        public InputParser()
        {
            InitializeComponent();
            DataContext = new InputParserModel();
        }
    }
}
