using System.Collections.Generic;

namespace ricaun.Revit.Mvvm
{
    /// <summary>
    /// Represents a dynamic data collection that provides notifications when items get 
    /// added, removed, or when the whole list is refreshed.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>
    {
        /// <summary>
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection`1 class.
        /// </summary>
        ObservableCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection`1 class.
        /// </summary>
        /// <param name="list"></param>
        public ObservableCollection(List<T> list) : base(list)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection`1 class.
        /// </summary>
        /// <param name="collection"></param>
        public ObservableCollection(IEnumerable<T> collection) : base(collection)
        {
        }
    }
}