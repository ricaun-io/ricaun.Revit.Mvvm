using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

// https://github.com/CommunityToolkit/WindowsCommunityToolkit/blob/rel/winui/7.1.2/CommunityToolkit.Mvvm/Input/AsyncRelayCommand.cs

namespace ricaun.Revit.Mvvm
{
    /// <summary>
    /// An interface expanding <see cref="IRelayCommand"/> to support asynchronous operations.
    /// </summary>
    public interface IAsyncRelayCommand : IRelayCommand
    {

    }

    /// <summary>
    /// A command that mirrors the functionality of <see cref="RelayCommand"/>, with the addition of
    /// accepting a <see cref="Func{TResult}"/> returning a <see cref="Task"/> as the execute
    /// action, and providing an <see cref="ExecutionTask"/> property that notifies changes when
    /// <see cref="ExecuteAsync"/> is invoked and when the returned <see cref="Task"/> completes.
    /// </summary>
    public sealed class AsyncRelayCommand : ObservableObject, IAsyncRelayCommand
    {
        /// <summary>
        /// The <see cref="Func{TResult}"/> to invoke when <see cref="Execute"/> is used.
        /// </summary>
        private readonly Func<Task> execute;

        /// <summary>
        /// The cancelable <see cref="Func{T,TResult}"/> to invoke when <see cref="Execute"/> is used.
        /// </summary>
        /// <remarks>Only one between this and <see cref="execute"/> is not <see langword="null"/>.</remarks>
        private readonly Func<CancellationToken, Task> cancelableExecute;

        /// <summary>
        /// The optional action to invoke when <see cref="CanExecute"/> is used.
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        /// The <see cref="CancellationTokenSource"/> instance to use to cancel <see cref="cancelableExecute"/>.
        /// </summary>
        /// <remarks>This is only used when <see cref="cancelableExecute"/> is not <see langword="null"/>.</remarks>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public AsyncRelayCommand(Func<Task> execute)
        {
            this.execute = execute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class that can always execute.
        /// </summary>
        /// <param name="cancelableExecute">The cancelable execution logic.</param>
        public AsyncRelayCommand(Func<CancellationToken, Task> cancelableExecute)
        {
            this.cancelableExecute = cancelableExecute;
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
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class.
        /// </summary>
        /// <param name="cancelableExecute">The cancelable execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public AsyncRelayCommand(Func<CancellationToken, Task> cancelableExecute, Func<bool> canExecute)
        {
            this.cancelableExecute = cancelableExecute;
            this.canExecute = canExecute;
        }

        private TaskNotifier executionTask;

        /// <inheritdoc/>
        public Task ExecutionTask
        {
            get => this.executionTask;
            private set
            {
                if (SetPropertyAndNotifyOnCompletion(ref this.executionTask, value, _ =>
                {

                }))
                {

                }
            }
        }

        /// <summary>
        /// CanExecuteChanged
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute?.Invoke() != false;
        }

        public void Execute(object parameter)
        {
            _ = ExecuteAsync(parameter);
        }

        public Task ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter))
            {
                // Non cancelable command delegate
                if (this.execute is not null)
                {
                    return ExecutionTask = this.execute();
                }

                // Cancel the previous operation, if one is pending
                this.cancellationTokenSource?.Cancel();

                CancellationTokenSource cancellationTokenSource = this.cancellationTokenSource = new();

                // Invoke the cancelable command delegate with a new linked token
                return ExecutionTask = this.cancelableExecute!(cancellationTokenSource.Token);
            }

            return Task.CompletedTask;
        }
    }
}
