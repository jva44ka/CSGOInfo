using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfLawyersSystem.Models;
using DevExpress.Mvvm;
using WpfLawyersSystem.Views;

namespace WpfLawyersSystem.ViewModels
{
    public class OnePlayerViewModel : BaseViewModel
    {
        Player _newPlayer;
        Player _selectedPlayer;
        ListOfTeams _listOfTeams;
        public Player NewPlayer
        {
            get { return _newPlayer; }
            set
            {
                _newPlayer = value;
                base.OnPropertyChanged();
            }
        }
        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;
                base.OnPropertyChanged();
            }
        }

        public ListOfTeams ListOfTeams
        {
            get { return _listOfTeams; }
            set
            {
                _listOfTeams = value;
                base.OnPropertyChanged();
            }
        }

        public OnePlayerViewModel() //ctor
        {
            _newPlayer = new Player();
            //_selectedPlayer = new Player();
            _listOfTeams = FactoryOfLists.ObjTeams;
        }
        public OnePlayerViewModel(Player player) //ctorParam
        {
            //_newPlayer = new Player();
            _selectedPlayer = player;
            _listOfTeams = FactoryOfLists.ObjTeams;
        }

        public ICommand bAdd_Command
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NewPlayer.Team.TheCrew.Add(_newPlayer);
                    FactoryOfLists.ObjPlayers.List.Add(NewPlayer);
                });
            }
        }
        public ICommand bRemove_Command
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SelectedPlayer.Team.TheCrew.Remove(_selectedPlayer);
                    FactoryOfLists.ObjPlayers.List.Remove(SelectedPlayer);
                });
            }
        }
    }
}
