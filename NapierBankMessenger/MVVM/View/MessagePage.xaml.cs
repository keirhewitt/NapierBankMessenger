using System;
using NapierBankMessenger.MVVM.Model;
using System.Windows.Controls;

namespace NapierBankMessenger.MVVM.View
{

    public partial class MessagePage : UserControl
    {

        public MessagePage()
        {
            InitializeComponent();
            DataContext = new Controller();
        }
    }
}
