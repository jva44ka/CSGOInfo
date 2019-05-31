using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WpfLawyersSystem.Models;

namespace WpfLawyersSystem.ViewModels
{
    public class OneTeamViewModel : NotifycationsPropertyChanged
    {

        Team _newTeam;
        Player[] _players; //игроки для подвязки к View
        bool[] _switchers; // флаг созданности того или иного игрока в составе newTeam. true = существует
        Team _selectedTeam;
        public Team NewTeam
        {
            get { return _newTeam; }
            set
            {
                _newTeam = value;
                base.OnPropertyChanged();
            }
        }
        public Player[] Players
        {
            get { return _players; }
            set
            {
                _players = value;
                base.OnPropertyChanged();
            }
        }
        public bool[] Swithers
        {
            get { return _switchers; }
            set
            {
                _switchers = value;
                base.OnPropertyChanged();
            }
        }
        public ObservableCollection<string> ButtonPlayersContent { get; set; }
        public Team SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                _selectedTeam = value;
                base.OnPropertyChanged();
            }
        }
        public OneTeamViewModel()
        {
            _newTeam = new Team();
            _players = new Player[5];
            _switchers = new bool[5];
            ButtonPlayersContent = new ObservableCollection<string>();
            for (int i = 0; i < 5; i++)
            {
                _switchers[i] = false;
                _players[i] = new Player();
                ButtonPlayersContent.Add("Создать");
            }
        }
        public OneTeamViewModel(Team team) //ctor
        {
            _selectedTeam = team;
            _switchers = new bool[5];
            _players = new Player[5];
            ButtonPlayersContent = new ObservableCollection<string>();
            for (int i = 0; i < 5; i++)
            {
                _switchers[i] = false;
                ButtonPlayersContent.Add("Создать");
            }
            for (int i = 0; i < _selectedTeam.TheCrew.Count; i++)
            {
                _switchers[i] = true;
                ButtonPlayersContent[i] = "Удалить";
                _players[i] = _selectedTeam.TheCrew[i];
            }
        }
        public ICommand bAdd_Command
        {
            get
            {
                return new RelayCommand(() =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (Swithers[i] == true)
                        {
                            //Player1.Team = _newTeam;
                            //_newTeam.TheCrew.Add(Players[i]);
                            FactoryOfLists.ObjPlayers.List.Add(new Player(Players[i]));
                        }
                    }
                    FactoryOfLists.ObjTeams.List.Add(new Team(NewTeam));
                });
            }
        }

        public ICommand bRemove_Command
        {
            get
            {
                return new RelayCommand(() =>
                FactoryOfLists.ObjTeams.List.Remove(_selectedTeam)
                );
            }
        }

        public ICommand bAddPlayerInTeam_Command
        {
            get
            {
                return new RelayCommand<string>((param) =>
                {
                    int playerNum = int.Parse(param);
                    if (Swithers[playerNum] == false)
                    { //проверка создан ли игрок

                        _newTeam.TheCrew.Add(Players[playerNum]);
                        Swithers[playerNum] = true;
                        ButtonPlayersContent[playerNum] = "Удалить";////////////
                    }
                    else
                    {
                        _newTeam.TheCrew.Remove(Players[playerNum]);
                        Players[playerNum] = null;
                        Swithers[playerNum] = false;
                        ButtonPlayersContent[playerNum] = "Создать";/////////////
                    }
                });
            }
        }
    }
}