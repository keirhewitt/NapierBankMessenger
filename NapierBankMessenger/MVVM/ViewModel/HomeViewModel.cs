using System;
using System.ComponentModel;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class HomeViewModel
    {
        public BaseViewModel BaseVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            BaseVM = new BaseViewModel();
        }
    }
}
