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
    public class MatchViewModel : BaseViewModel
    {
        private ListOfMatches _listOfMatches;
        private string _searchText;
        public ListOfMatches ListOfMatches
        {
            get { return _listOfMatches; }
            set
            {
                _listOfMatches = value;
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }
        public MatchViewModel()
        {
            _listOfMatches = FactoryOfLists.ObjMatches;

        }

    }
}
