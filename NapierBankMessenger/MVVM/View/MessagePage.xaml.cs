using System;
using NapierBankMessenger.MVVM.Model;
using System.Windows.Controls;

namespace NapierBankMessenger.MVVM.View
{

    public partial class HomePage : UserControl
    {

        public HomePage()
        {
            InitializeComponent();
            DataContext = new Controller();
        }
    }
}
