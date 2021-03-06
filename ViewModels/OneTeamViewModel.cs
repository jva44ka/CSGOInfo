﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WpfLawyersSystem.Models;
using DevExpress.Mvvm;

namespace WpfLawyersSystem.ViewModels
{
    public class OneTeamViewModel : BaseViewModel
    {
        /// <summary>
        /// Поля
        /// </summary>
        Team _newTeam; // новая команда
        Player[] _players; //игроки для подвязки к View
        bool[] _switchers; // флаг созданности того или иного игрока в составе newTeam. true = существует
        Team _selectedTeam; // выбранная команда (для изменения)

        /// <summary>
        /// /////////Свойства
        /// </summary>
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

        /// <summary>
        /// //////// Конструкторы
        /// </summary>
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
                _players[i].Team = _newTeam;
                ButtonPlayersContent.Add("Создать");
            }
        }
        public OneTeamViewModel(Team team) //ctor
        {
            _selectedTeam = team;
            _switchers = new bool[5];
            _players = new Player[5];
            ButtonPlayersContent = new ObservableCollection<string>();
            for (int i = 0; i < _selectedTeam.TheCrew.Count; i++)
            {
                _switchers[i] = true;
                ButtonPlayersContent.Add("Удалить");
                _players[i] = _selectedTeam.TheCrew[i];
            }
            for (int i = _selectedTeam.TheCrew.Count; i < 5; i++)
            {
                _switchers[i] = false;
                ButtonPlayersContent.Add("Создать");
                _players[i] = new Player();
                _players[i].Team = _selectedTeam;
            }
        }

        /// <summary>
        /// ////// Команды
        /// </summary>
        public ICommand bAdd_Command
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    FactoryOfLists.ObjTeams.List.Add(NewTeam);
                });
            }
        }

        public ICommand bRemove_Command
        {
            get
            {
                return new DelegateCommand(() =>
                FactoryOfLists.ObjTeams.List.Remove(_selectedTeam)
                );
            }
        }

        public ICommand bCreateOrDeletePlayer_Command
        {
            get
            {
                return new DelegateCommand<string>((param) =>
                {
                    int playerNum = int.Parse(param); // Какого игрока меняем / удаляем
                    Team thisTeam; // Меняем или удаляем?

                    _ = SelectedTeam != null ? thisTeam = SelectedTeam : thisTeam = NewTeam;

                    if (Swithers[playerNum] == false)
                    { //проверка создан ли игрок
                        thisTeam.TheCrew.Add(Players[playerNum]);
                        Swithers[playerNum] = true;
                        ButtonPlayersContent[playerNum] = "Удалить";
                        FactoryOfLists.ObjPlayers.List.Add(Players[playerNum]);
                    }
                    else
                    {
                        int[] array = new int[5];
                        //thisTeam.TheCrew.Remove(Players[playerNum]);
                        Players[playerNum] = new Player();
                        Players[playerNum].Team = thisTeam;
                        Swithers[playerNum] = false;
                        ButtonPlayersContent[playerNum] = "Создать";
                        FactoryOfLists.ObjPlayers.List.Remove(Players[playerNum]);
                        thisTeam.TheCrew.Remove(Players[playerNum]);
                    }
                    OnPropertyChanged(thisTeam.Name);
                });
            }
        }
    }
}