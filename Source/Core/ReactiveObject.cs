﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Slithin.Core
{
    public class ReactiveObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetValue<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
