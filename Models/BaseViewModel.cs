using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfLawyersSystem.Models
{
    
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Реализация INotifyPropertyChanged для всех дочерних ViewModel
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
