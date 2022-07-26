// This file is inspired from the Microsoft.Toolkit.Mvvm library and AsyncVoid
// https://github.com/CommunityToolkit/WindowsCommunityToolkit
// https://github.com/johnthiriet/AsyncVoid

using System;
using System.Threading.Tasks;

namespace ricaun.Revit.Mvvm.Extensions
{
    /// <summary>
    /// TaskExtension
    /// </summary>
    internal static class TaskExtension
    {
        internal static async void Run(this Task task, Action<Exception> handleException)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handleException?.Invoke(ex);
            }
        }
    }
}
