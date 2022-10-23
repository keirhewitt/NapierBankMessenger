using System;
using NapierBankMessenger.MVVM.Model;
using System.Windows.Controls;
using Caliburn.Micro;

namespace NapierBankMessenger.MVVM.View
{

    public partial class HomePage : UserControl
    {
        public BindableCollection<User> users { get; set; }

        public HomePage()
        {
            InitializeComponent();
        }
    }
}
