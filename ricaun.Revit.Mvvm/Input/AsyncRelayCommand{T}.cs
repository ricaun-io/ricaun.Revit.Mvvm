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
    /// A generic command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is <see langword="true"/>. This class allows you to accept command parameters
    /// in the <see cref="Execute(T)"/> and <see cref="CanExecute(T)"/> callback methods.
    /// </summary>
    /// <typeparam name="T">The type of parameter being passed as input to the callbacks.</typeparam>
    public sealed class AsyncRelayCommand<T> : IAsyncRelayCommand<T>
    {
        /// <summary>
        /// The <see cref="Action"/> to invoke when <see cref="Execute(T)"/> is used.
        /// </summary>
        private readonly Func<T, Task> execute;


        /// <summary>
        /// The optional action to invoke when <see cref="CanExecute(T)"/> is used.
        /// </summary>
        private readonly Func<T, bool> canExecute;

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
        /// Initializes a new instance of the <see cref="AsyncRelayCommand{T}"/> class that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <remarks>
        /// Due to the fact that the <see cref="System.Windows.Input.ICommand"/> interface exposes methods that accept a
        /// nullable <see cref="object"/> parameter, it is recommended that if <typeparamref name="T"/> is a reference type,
        /// you should always declare it as nullable, and to always perform checks within <paramref name="execute"/>.
        /// </remarks>
        public AsyncRelayCommand(Func<T, Task> execute)
        {
            this.execute = execute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <remarks>See notes in <see cref="AsyncRelayCommand{T}(Func{T, Task})"/>.</remarks>
        public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// CanExecute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(T parameter)
        {
            return this.canExecute?.Invoke(parameter) != false;
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

            if (parameter is T parameterT)
                return CanExecute(parameterT);

            return false;
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(T parameter)
        {
            this.ExecuteAsync(parameter).Run();
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (parameter is T parameterT)
                Execute(parameterT);
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
        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                if (this.execute is not null)
                {
                    try
                    {
                        IsExecuting = true;
                        await this.execute(parameter);
                    }
                    finally
                    {
                        IsExecuting = false;
                    }
                }
            }

            RaiseCanExecuteChanged();
        }
    }
}
