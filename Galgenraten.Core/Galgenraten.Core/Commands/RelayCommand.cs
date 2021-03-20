using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Galgenraten.Core.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action<object> _executeHandler;
        private readonly Predicate<object> _canExecuteHandler;

        #region konstruktoren 
        public RelayCommand(Action<object> execute) : this(execute,null) { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _executeHandler = execute;
            _canExecuteHandler = canExecute;
        }
        #endregion

        public bool CanExecute(object parameter)
        {
            if (_canExecuteHandler == null)
                return true;

            return _canExecuteHandler(parameter);
        }

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }
    }
}
