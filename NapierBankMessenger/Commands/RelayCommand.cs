using System;
using System.Windows.Input;

namespace NapierBankMessenger.Commands
{
    internal class RelayCommand : ICommand
    {
        private Action _execute;
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }


        public bool CanExecute(object param)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
