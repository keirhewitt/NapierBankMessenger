using System;
using System.Windows.Input;

namespace NapierBankMessenger.Commands
{
    internal class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Constructor that takes a predicate to determine whether the Action `execute` can run
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object param)
        {
            if (_canExecute == null)
                return true;
            return _canExecute(param);
        }

        public void Execute(object param)
        {
            _execute.Invoke(param);
        }
    }
}
