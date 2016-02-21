using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.Util
{
    public class RelayCommand : CommandBase
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute)
            : this(execute, null)
        { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        protected override void Execute()
        {
            _execute();
        }

        public override bool CanExecute
        {
            get { return _canExecute == null || _canExecute(); }
        }
    }

    public class RelayCommand<T> : CommandBase<T>
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;
        
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(T parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        protected override void Execute(T parameter)
        {
            _execute(parameter);
        }
    }
}
