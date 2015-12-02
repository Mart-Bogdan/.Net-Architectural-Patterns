using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WorkWithDB.UI
{
    public static class RelayCommand
    {
        public static RelayCommand<T> Create<T>(Action<T> execute, Predicate<T> canExecute)
        {
            return new RelayCommand<T>(execute, canExecute);
        }

        public static RelayCommand<T> Create<T>(Action<T> execute)
        {
            return new RelayCommand<T>(execute);
        }

        public static RelayCommand<Object> CreateVoid(Action execute, Func<bool> canExecute)
        {
            return Create<Object>(_ => execute(), _ => canExecute());
        }

        public static RelayCommand<Object> CreateVoid(Action execute)
        {
            return Create<Object>(_ => execute());
        }
        
    }
    public class RelayCommand<T> : ICommand
    {

        #region Fields

        readonly Action<T> execute;
        readonly Predicate<T> canExecute;

        #endregion

        #region Constructors

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute((T) parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute((T) parameter);
        }

        #endregion
    }
}
