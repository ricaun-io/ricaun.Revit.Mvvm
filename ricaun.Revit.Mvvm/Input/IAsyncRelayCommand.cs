// This file is inspired from the Microsoft.Toolkit.Mvvm library and AsyncVoid
// https://github.com/CommunityToolkit/WindowsCommunityToolkit
// https://github.com/johnthiriet/AsyncVoid

namespace ricaun.Revit.Mvvm
{
    /// <summary>
    /// An interface expanding <see cref="IRelayCommand"/> to support asynchronous operations.
    /// </summary>
    public interface IAsyncRelayCommand : IRelayCommand
    {
        bool IsExecuting { get; }
    }
}
