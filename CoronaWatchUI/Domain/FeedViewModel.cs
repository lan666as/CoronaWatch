﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoronaWatchUI.Domain
{
    public class FeedViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
