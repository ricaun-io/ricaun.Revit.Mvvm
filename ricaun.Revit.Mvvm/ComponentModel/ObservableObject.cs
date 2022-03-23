﻿using System;
using System.ComponentModel;

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
    }
}