using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Outliner.Util
{
    public abstract class CommandBase : ObservableObject, ICommand
    {
        public abstract bool CanExecute { get; }
        protected abstract void Execute();

        public bool TryExecute()
        {
            if (!CanExecute)
                return false;
            Execute();
            return true;
        }        

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        public virtual void RaiseCanExecuteChanged()
        {
            RaisePropertyChanged(nameof(CanExecute));

            var copy = CanExecuteChanged;
            if (copy != null)
                copy(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;
    }

    public abstract class CommandBase<T> : ICommand
    {
        public abstract bool CanExecute(T parameter);
        protected abstract void Execute(T parameter);

        public bool TryExecute(T parameter)
        {
            if (!CanExecute(parameter))
                return false;

            Execute(parameter);
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        public virtual void RaiseCanExecuteChanged()
        {
            var copy = CanExecuteChanged;
            if (copy != null)
                copy(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;
    }
}
