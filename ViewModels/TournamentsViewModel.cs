using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfLawyersSystem.Models;

namespace WpfLawyersSystem.ViewModels
{
    public class TournamentsViewModel : INotifyPropertyChanged
    {
        private ListOfTournaments _listOfTournaments;
        public ListOfTournaments ListOfTournaments
        {
            get { return _listOfTournaments; }
            set
            {
                _listOfTournaments = value;
                OnPropertyChanged();
            }
        }
        public TournamentsViewModel()
        {
            _listOfTournaments = FactoryOfLists.ObjTournaments;
        }


        /// <summary>
        /// Реализация INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
