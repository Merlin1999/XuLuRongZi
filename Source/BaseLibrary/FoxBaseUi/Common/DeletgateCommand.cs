using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FoxBaseUi.Common
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(Action<object> executeMethod)
            : this(executeMethod, null)
        { }

        public DelegateCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod) 
        {
            if (executeMethod == null)
                throw new ArgumentNullException("executeMetnod");
            this._executeCommand = executeMethod;
            this._canExecuteCommand = canExecuteMethod;
        }

        //A method prototype without return value.
        private readonly Action<object> _executeCommand = null;
        //A method prototype return a bool type.
        private readonly Func<object, bool> _canExecuteCommand = null;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecuteCommand != null)
            {
                return this._canExecuteCommand(parameter);
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            if (this._executeCommand != null) this._executeCommand(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
