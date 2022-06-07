using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ricaun.Revit.Mvvm
{
    /// <summary>
    /// ObservableObject
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Event that gets fired when any property changes on child classes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Refresh a single property to UI
        /// </summary>
        /// <param name="propertyName"></param>
        public void RefreshProperty(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Refresh all properties to UI
        /// </summary>
        public void RefreshAllProperties()
        {
            System.Reflection.PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                RefreshProperty(property.Name);
            }
        }

        #region Changes

        /// <summary>
        /// Compares the current and new values for a given field (which should be the backing
        /// field for a property). If the value has changed, raises the <see cref="PropertyChanging"/>
        /// event, updates the field and then raises the <see cref="PropertyChanged"/> event.
        /// The behavior mirrors that of <see cref="SetProperty{T}(ref T,T,string)"/>, with the difference being that
        /// this method will also monitor the new value of the property (a generic <see cref="Task"/>) and will also
        /// raise the <see cref="PropertyChanged"/> again for the target property when it completes.
        /// This can be used to update bindings observing that <see cref="Task"/> or any of its properties.
        /// This method and its overload specifically rely on the <see cref="TaskNotifier"/> type, which needs
        /// to be used in the backing field for the target <see cref="Task"/> property. The field doesn't need to be
        /// initialized, as this method will take care of doing that automatically. The <see cref="TaskNotifier"/>
        /// type also includes an implicit operator, so it can be assigned to any <see cref="Task"/> instance directly.
        /// Here is a sample property declaration using this method:
        /// <code>
        /// private TaskNotifier myTask;
        ///
        /// public Task MyTask
        /// {
        ///     get => myTask;
        ///     private set => SetAndNotifyOnCompletion(ref myTask, value);
        /// }
        /// </code>
        /// </summary>
        /// <param name="taskNotifier">The field notifier to modify.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> events are not raised if the current
        /// and new value for the target property are the same. The return value being <see langword="true"/> only
        /// indicates that the new value being assigned to <paramref name="taskNotifier"/> is different than the previous one,
        /// and it does not mean the new <see cref="Task"/> instance passed as argument is in any particular state.
        /// </remarks>
        protected bool SetPropertyAndNotifyOnCompletion(ref TaskNotifier taskNotifier, Task newValue, [CallerMemberName] string propertyName = null)
        {
            // We invoke the overload with a callback here to avoid code duplication, and simply pass an empty callback.
            // The lambda expression here is transformed by the C# compiler into an empty closure class with a
            // static singleton field containing a closure instance, and another caching the instantiated Action<TTask>
            // instance. This will result in no further allocations after the first time this method is called for a given
            // generic type. We only pay the cost of the virtual call to the delegate, but this is not performance critical
            // code and that overhead would still be much lower than the rest of the method anyway, so that's fine.
            return SetPropertyAndNotifyOnCompletion(taskNotifier ??= new(), newValue, static _ => { }, propertyName);
        }

        /// <summary>
        /// Compares the current and new values for a given field (which should be the backing
        /// field for a property). If the value has changed, raises the <see cref="PropertyChanging"/>
        /// event, updates the field and then raises the <see cref="PropertyChanged"/> event.
        /// This method is just like <see cref="SetPropertyAndNotifyOnCompletion(ref TaskNotifier,Task,string)"/>,
        /// with the difference being an extra <see cref="Action{T}"/> parameter with a callback being invoked
        /// either immediately, if the new task has already completed or is <see langword="null"/>, or upon completion.
        /// </summary>
        /// <param name="taskNotifier">The field notifier to modify.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="callback">A callback to invoke to update the property value.</param>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> events are not raised
        /// if the current and new value for the target property are the same.
        /// </remarks>
        protected bool SetPropertyAndNotifyOnCompletion(ref TaskNotifier taskNotifier, Task newValue, Action<Task> callback, [CallerMemberName] string propertyName = null)
        {
            return SetPropertyAndNotifyOnCompletion(taskNotifier ??= new(), newValue, callback, propertyName);
        }

        /// <summary>
        /// Compares the current and new values for a given field (which should be the backing
        /// field for a property). If the value has changed, raises the <see cref="PropertyChanging"/>
        /// event, updates the field and then raises the <see cref="PropertyChanged"/> event.
        /// The behavior mirrors that of <see cref="SetProperty{T}(ref T,T,string)"/>, with the difference being that
        /// this method will also monitor the new value of the property (a generic <see cref="Task"/>) and will also
        /// raise the <see cref="PropertyChanged"/> again for the target property when it completes.
        /// This can be used to update bindings observing that <see cref="Task"/> or any of its properties.
        /// This method and its overload specifically rely on the <see cref="TaskNotifier{T}"/> type, which needs
        /// to be used in the backing field for the target <see cref="Task"/> property. The field doesn't need to be
        /// initialized, as this method will take care of doing that automatically. The <see cref="TaskNotifier{T}"/>
        /// type also includes an implicit operator, so it can be assigned to any <see cref="Task"/> instance directly.
        /// Here is a sample property declaration using this method:
        /// <code>
        /// private TaskNotifier&lt;int&gt; myTask;
        ///
        /// public Task&lt;int&gt; MyTask
        /// {
        ///     get => myTask;
        ///     private set => SetAndNotifyOnCompletion(ref myTask, value);
        /// }
        /// </code>
        /// </summary>
        /// <typeparam name="T">The type of result for the <see cref="Task{TResult}"/> to set and monitor.</typeparam>
        /// <param name="taskNotifier">The field notifier to modify.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> events are not raised if the current
        /// and new value for the target property are the same. The return value being <see langword="true"/> only
        /// indicates that the new value being assigned to <paramref name="taskNotifier"/> is different than the previous one,
        /// and it does not mean the new <see cref="Task{TResult}"/> instance passed as argument is in any particular state.
        /// </remarks>
        protected bool SetPropertyAndNotifyOnCompletion<T>(ref TaskNotifier<T> taskNotifier, Task<T> newValue, [CallerMemberName] string propertyName = null)
        {
            return SetPropertyAndNotifyOnCompletion(taskNotifier ??= new(), newValue, static _ => { }, propertyName);
        }

        /// <summary>
        /// Compares the current and new values for a given field (which should be the backing
        /// field for a property). If the value has changed, raises the <see cref="PropertyChanging"/>
        /// event, updates the field and then raises the <see cref="PropertyChanged"/> event.
        /// This method is just like <see cref="SetPropertyAndNotifyOnCompletion{T}(ref TaskNotifier{T},Task{T},string)"/>,
        /// with the difference being an extra <see cref="Action{T}"/> parameter with a callback being invoked
        /// either immediately, if the new task has already completed or is <see langword="null"/>, or upon completion.
        /// </summary>
        /// <typeparam name="T">The type of result for the <see cref="Task{TResult}"/> to set and monitor.</typeparam>
        /// <param name="taskNotifier">The field notifier to modify.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="callback">A callback to invoke to update the property value.</param>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> events are not raised
        /// if the current and new value for the target property are the same.
        /// </remarks>
        protected bool SetPropertyAndNotifyOnCompletion<T>(ref TaskNotifier<T> taskNotifier, Task<T> newValue, Action<Task<T>> callback, [CallerMemberName] string propertyName = null)
        {
            return SetPropertyAndNotifyOnCompletion(taskNotifier ??= new(), newValue, callback, propertyName);
        }

        /// <summary>
        /// Implements the notification logic for the related methods.
        /// </summary>
        /// <typeparam name="TTask">The type of <see cref="Task"/> to set and monitor.</typeparam>
        /// <param name="taskNotifier">The field notifier.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="callback">A callback to invoke to update the property value.</param>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
        private bool SetPropertyAndNotifyOnCompletion<TTask>(ITaskNotifier<TTask> taskNotifier, TTask newValue, Action<TTask> callback, [CallerMemberName] string propertyName = null)
            where TTask : Task
        {
            if (ReferenceEquals(taskNotifier.Task, newValue))
            {
                return false;
            }

            // Check the status of the new task before assigning it to the
            // target field. This is so that in case the task is either
            // null or already completed, we can avoid the overhead of
            // scheduling the method to monitor its completion.
            bool isAlreadyCompletedOrNull = newValue?.IsCompleted ?? true;

            taskNotifier.Task = newValue;

            RefreshProperty(propertyName);

            // If the input task is either null or already completed, we don't need to
            // execute the additional logic to monitor its completion, so we can just bypass
            // the rest of the method and return that the field changed here. The return value
            // does not indicate that the task itself has completed, but just that the property
            // value itself has changed (ie. the referenced task instance has changed).
            // This mirrors the return value of all the other synchronous Set methods as well.
            if (isAlreadyCompletedOrNull)
            {
                callback(newValue);

                return true;
            }

            // We use a local async function here so that the main method can
            // remain synchronous and return a value that can be immediately
            // used by the caller. This mirrors Set<T>(ref T, T, string).
            // We use an async void function instead of a Task-returning function
            // so that if a binding update caused by the property change notification
            // causes a crash, it is immediately reported in the application instead of
            // the exception being ignored (as the returned task wouldn't be awaited),
            // which would result in a confusing behavior for users.
            async void MonitorTask()
            {
                try
                {
                    // Await the task and ignore any exceptions
                    await newValue!;
                }
                catch
                {
                }

                // Only notify if the property hasn't changed
                if (ReferenceEquals(taskNotifier.Task, newValue))
                {
                    RefreshProperty(propertyName);
                }

                callback(newValue);
            }

            MonitorTask();

            return true;
        }

        /// <summary>
        /// An interface for task notifiers of a specified type.
        /// </summary>
        /// <typeparam name="TTask">The type of value to store.</typeparam>
        private interface ITaskNotifier<TTask>
            where TTask : Task
        {
            /// <summary>
            /// Gets or sets the wrapped <typeparamref name="TTask"/> value.
            /// </summary>
            TTask Task { get; set; }
        }

        /// <summary>
        /// A wrapping class that can hold a <see cref="Task"/> value.
        /// </summary>
        protected sealed class TaskNotifier : ITaskNotifier<Task>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TaskNotifier"/> class.
            /// </summary>
            internal TaskNotifier()
            {
            }

            private Task task;

            /// <inheritdoc/>
            Task ITaskNotifier<Task>.Task
            {
                get => this.task;
                set => this.task = value;
            }

            /// <summary>
            /// Unwraps the <see cref="Task"/> value stored in the current instance.
            /// </summary>
            /// <param name="notifier">The input <see cref="TaskNotifier{TTask}"/> instance.</param>
            public static implicit operator Task(TaskNotifier notifier)
            {
                return notifier?.task;
            }
        }

        /// <summary>
        /// A wrapping class that can hold a <see cref="Task{T}"/> value.
        /// </summary>
        /// <typeparam name="T">The type of value for the wrapped <see cref="Task{T}"/> instance.</typeparam>
        protected sealed class TaskNotifier<T> : ITaskNotifier<Task<T>>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TaskNotifier{TTask}"/> class.
            /// </summary>
            internal TaskNotifier()
            {
            }

            private Task<T> task;

            /// <inheritdoc/>
            Task<T> ITaskNotifier<Task<T>>.Task
            {
                get => this.task;
                set => this.task = value;
            }

            /// <summary>
            /// Unwraps the <see cref="Task{T}"/> value stored in the current instance.
            /// </summary>
            /// <param name="notifier">The input <see cref="TaskNotifier{TTask}"/> instance.</param>
            public static implicit operator Task<T>(TaskNotifier<T> notifier)
            {
                return notifier?.task;
            }
        }
    }
    #endregion
}