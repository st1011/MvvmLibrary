using System;
using System.Windows.Input;

namespace MvvmLibrary
{
    public class DelegateCommand<T> : ICommand
    {
        /// <summary>
        /// コマンドの実体
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// コマンドの実行可否の実体
        /// </summary>
        private readonly Func<T, bool> _canExecute;

        private static readonly Func<T, bool> _allwaysExecutable = _ => true;

        public DelegateCommand(Action execute)
            : this(execute, null) { }

        public DelegateCommand(Action execute, Func<bool> canExecute)
            : this(_ => execute?.Invoke(), _ => canExecute?.Invoke() ?? true) { }

        public DelegateCommand(Action<T> execute)
            : this(execute, null) { }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute ?? _allwaysExecutable;
        }

        /// <summary>
        /// CanExecuteChangedイベントを発行
        /// </summary>
        public static void RaiseCanExecuteChanged()
            => CommandManager.InvalidateRequerySuggested();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// コマンド実行可否
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;

            return _canExecute.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke((T)parameter);
        }
    }

    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action execute) : base(execute)
        {
        }

        public DelegateCommand(Action<object> execute) : base(execute)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExecute) : base(execute, canExecute)
        {
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }
    }
}
