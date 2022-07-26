// This file is inspired from the Microsoft.Toolkit.Mvvm library and AsyncVoid
// https://github.com/CommunityToolkit/WindowsCommunityToolkit
// https://github.com/johnthiriet/AsyncVoid

using ricaun.Revit.Mvvm.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ricaun.Revit.Mvvm
{
    /// <summary>
    /// A command that mirrors the functionality of <see cref="RelayCommand"/>, with the addition of
    /// accepting a <see cref="Func{TResult}"/> returning a <see cref="Task"/> as the execute
    /// action, and providing an <see cref="IsExecuting"/> property that notifies changes when
    /// <see cref="ExecuteAsync"/> is invoked and when the returned <see cref="Task"/> completes.
    /// </summary>
    public sealed class AsyncRelayCommand : ObservableObject, IAsyncRelayCommand
    {
        /// <summary>
        /// The <see cref="Action"/> to invoke when <see cref="Execute"/> is used.
        /// </summary>
        private readonly Func<Task> execute;

        /// <summary>
        /// The optional action to invoke when <see cref="CanExecute"/> is used.
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        /// The optional action to Handle Exception
        /// </summary>
        private Action<Exception> handleException;

        /// <summary>
        /// Set Exception Handler 
        /// </summary>
        /// <param name="handleException"></param>
        /// <returns></returns>
        public AsyncRelayCommand SetExceptionHandler(Action<Exception> handleException)
        {
            this.handleException = handleException;
            return this;
        }

        /// <summary>
        /// CanExecuteChanged
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// RaiseCanExecuteChanged
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public AsyncRelayCommand(Func<Task> execute)
        {
            this.execute = execute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// CanExecute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (IsExecuting)
                return false;

            return this.canExecute?.Invoke() != false;
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this.ExecuteAsync(parameter).Run(handleException);
        }

        /// <summary>
        /// Command is executing
        /// </summary>
        public bool IsExecuting { get; private set; }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter))
            {
                if (this.execute is not null)
                {
                    try
                    {
                        IsExecuting = true;
                        await this.execute();
                    }
                    finally
                    {
                        IsExecuting = false;
                        RaiseCanExecuteChanged();
                    }
                }
            }
        }
    }
}
