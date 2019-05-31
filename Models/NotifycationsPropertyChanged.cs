using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfLawyersSystem.Models
{
    
    public class NotifycationsPropertyChanged : INotifyPropertyChanged
    {
        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
