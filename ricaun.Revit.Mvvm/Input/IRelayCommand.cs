// This file is inspired from the Microsoft.Toolkit.Mvvm library
// https://github.com/CommunityToolkit/WindowsCommunityToolkit

using System;
using System.Windows.Input;

namespace ricaun.Revit.Mvvm
{
    /// <summary>
    /// An interface expanding <see cref="ICommand"/> with the ability to raise
    /// the <see cref="ICommand.CanExecuteChanged"/> event externally.
    /// </summary>
    public interface IRelayCommand : ICommand
    {
        /// <summary>
        /// The optional action to Handle Exception
        /// </summary>
        public event Action<Exception> ExceptionHandler;
    }
}