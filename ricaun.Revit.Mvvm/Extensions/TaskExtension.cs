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
    public static class TaskExtension
    {
        public static async void Run(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
        public interface IErrorHandler
        {
            void HandleError(Exception ex);
        }
    }
}
