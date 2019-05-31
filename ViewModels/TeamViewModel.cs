using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfLawyersSystem.Models;
using WpfLawyersSystem.Views;

namespace WpfLawyersSystem.ViewModels
{
    public class TeamViewModel : NotifycationsPropertyChanged
    {
        private ListOfTeams _listOfTeams;
        private Team _selectedTeam;
        private string _searchText;
        public ListOfTeams ListOfTeams
        {
            get { return _listOfTeams; }
            set
            {
                _listOfTeams = value;
                base.OnPropertyChanged();
            }
        }
        public Team SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                _selectedTeam = value;
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                ListOfTeams = FactoryOfLists.ObjTeams.FindName(_searchText);
                OnPropertyChanged();
            }
        }
        public TeamViewModel() // ctor
        {
            _listOfTeams = FactoryOfLists.ObjTeams;

        }

        public ICommand bOpenSelectedItem_Command
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Views.ChangeTeamWindow newWindow = new Views.ChangeTeamWindow(SelectedTeam);
                    newWindow.ShowDialog();
                });
            }
        }
    }
}
