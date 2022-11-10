
using System.Windows.Controls;
using NapierBankMessenger.MVVM.Model;
using NapierBankMessenger.MVVM.ViewModel;

namespace NapierBankMessenger.MVVM.View
{

    public partial class MessagePage : UserControl
    {

        public MessagePage(Controller ctrl)
        {
            InitializeComponent();
            DataContext = new MessagePageViewModel(ctrl);
        }
    }
}
