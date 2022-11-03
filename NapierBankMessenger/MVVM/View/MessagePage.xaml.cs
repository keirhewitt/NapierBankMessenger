
using System.Windows.Controls;
using NapierBankMessenger.MVVM.ViewModel;

namespace NapierBankMessenger.MVVM.View
{

    public partial class MessagePage : UserControl
    {

        public MessagePage()
        {
            InitializeComponent();
            DataContext = new MessagePageController();
        }
    }
}
