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
using System.Windows.Controls;

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
        public Team SelectedPlayerTeam
        {
            get { return _selectedPlayer.Team; }
            set
            {
                SelectedPlayer.Team.TheCrew.Remove(SelectedPlayer);
                SelectedPlayer.Team = value;
                
                value.TheCrew.Add(SelectedPlayer);
                /*
                if (SelectedPlayer.Team.TheCrew != null)
                {
                    foreach (var item in SelectedPlayer.Team.TheCrew)
                    {
                        item.Team = SelectedPlayer.Team;
                    }
                }
                */
                base.OnPropertyChanged();
                base.OnPropertyChanged("SelectedPlayer.Team");
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
            _listOfTeams = FactoryOfLists.ObjTeams;
        }

        public OnePlayerViewModel(Player player) //ctorParam
        {
            _selectedPlayer = player;
            _listOfTeams = FactoryOfLists.ObjTeams;
        }

        public ICommand bAdd_Command
        {
            get
            {
                return new DelegateCommand<Window>((w) =>
                { // o - параметр, окно, из которого произошел вызов команды по кнопке
                    NewPlayer.Team.TheCrew.Add(_newPlayer);
                    FactoryOfLists.ObjPlayers.List.Add(NewPlayer);
                    w.Close();
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

        public ICommand bClose_Command
        {
            get
            {
                return new DelegateCommand<Window>((w) => { w.Close(); } );
            }
        }
    }
}
